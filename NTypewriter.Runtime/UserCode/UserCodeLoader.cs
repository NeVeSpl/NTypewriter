using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Functions;
using NTypewriter.Editor.Config;

namespace NTypewriter.Runtime.UserCode
{
    public class Result
    {
        public IEditorConfig Config { get; set; } = new EditorConfig();
        public IEnumerable<Type> TypesThatMayContainCustomFunctions = new List<Type>();
    }

    

    public class UserCodeLoader
    {
        private static readonly Dictionary<string, CacheItem> cache = new Dictionary<string, CacheItem>();
        private readonly IOutput output;
        private readonly IFileSearcher fileSearcher;

        public UserCodeLoader(IOutput output, IFileSearcher fileSearcher)
        {
            this.output = output;
            this.fileSearcher = fileSearcher;
        }


        public async Task<Result> LoadUserCodeForGivenProject(Solution solution, string projectFilePath)
        {
            output.Info($"Looking for user code in {Path.GetFileName(projectFilePath)}");

            var result = new Result();
            var project = solution.Projects.FirstOrDefault(x => x.FilePath == projectFilePath);

            if (project == null)
            {
                return result;
            }

            await CheckUsedAssemblyVersions(project);           
      
            var syntaxTreesFromRoslyn = await GetDecoratedSyntaxTreesWithAttribute(project.Documents, nameof(NTEditorFileAttribute));
            var userCodeFilePaths = fileSearcher.FindPaths(projectFilePath, ".nt.cs");
            var syntaxTreesFromFileSearch = GetSyntaxTrees(userCodeFilePaths).ToList();
            var syntaxTreesToCompile = JoinAndRemoveDuplicates(syntaxTreesFromRoslyn, syntaxTreesFromFileSearch);

            output.Info("Detected *.nt.cs files  : " + String.Join(",", syntaxTreesToCompile.Select(x => Path.GetFileName(x.FilePath))));

            if (cache.TryGetValue(projectFilePath, out var cached))
            {                
                if (cached.AreTheSame(userCodeFilePaths))
                {
                    output.Info("Files that contain user code have not changed, skiping compilation, using version from cache");
                    return cached.Result;
                }
            }

            var compilation = PrepareCompilation(syntaxTreesToCompile, "Configuration");
          
            using (MemoryStream ms = new MemoryStream(), ss = new MemoryStream())
            {
                var emitOptions = new EmitOptions(false, DebugInformationFormat.PortablePdb);
                var emitResult = compilation.Emit(ms, ss, options: emitOptions);
                if (emitResult.Success)
                {
                    var configAssembly = Assembly.Load(ms.ToArray(), ss.ToArray());
                    var globalConfigType = GetFirstTypeThatImplementsGivenInterface(configAssembly, nameof(IEditorConfig));

                    if (globalConfigType == null)
                    {
                        output.Info($"Class with global configuration was not found, using default global configuration instead");
                    }
                    else
                    {
                        output.Info($"Class with global configuration was found : {globalConfigType.Name}");

                        var globalConfigInstance = Activator.CreateInstance(globalConfigType);
                        var config = globalConfigInstance as IEditorConfig;
                        if (config == null)
                        {
                            output.Error($"Class {globalConfigType.Name} does not implement IEditorConfig interface, using default global configuration instead");                            
                        }
                        else
                        {
                            result.Config = config;
                        }
                    }

                    result.TypesThatMayContainCustomFunctions = configAssembly.GetTypes().Where(x => x.IsClass).Where(x => x.CustomAttributes.All(y => y.AttributeType.Name != "CompilerGeneratedAttribute")).ToList();

                    output.Info("User code loaded successfully");
                    cache[projectFilePath] = new CacheItem(userCodeFilePaths, result);
                    return result;
                }
                else
                {
                    var listOfErrors = new List<string>() { "Failed to compile user code" };

                    foreach (var diagnostic in emitResult.Diagnostics)
                    {
                        if (diagnostic.Severity == DiagnosticSeverity.Error)
                        {
                            listOfErrors.Add(diagnostic.ToString());
                        }
                    }

                    throw new RuntimeException(String.Join(Environment.NewLine, listOfErrors));
                }
            }
        }

        private IEnumerable<SyntaxTree> JoinAndRemoveDuplicates(IEnumerable<SyntaxTree> syntaxTreesFromRoslyn, IEnumerable<SyntaxTree> syntaxTreesFromFileSearch)
        {
            foreach(var syntaxTree in syntaxTreesFromRoslyn)
            {
                yield return syntaxTree;
            }
            var alreadyLodedPaths = new HashSet<string>(syntaxTreesFromRoslyn.Select(x => x.FilePath));
            foreach(var syntaxTree in syntaxTreesFromFileSearch)
            {
                if(!alreadyLodedPaths.Contains(syntaxTree.FilePath))
                {
                    yield return syntaxTree;
                }
            }
        }

        private async Task CheckUsedAssemblyVersions(Project project)
        {
            var compilation = await project.GetCompilationAsync();
            ShowWarningIfAssemblyVersionDoesNotMatch(compilation, typeof(EditorConfig));
            ShowWarningIfAssemblyVersionDoesNotMatch(compilation, typeof(ICodeModel));
        }
        private void ShowWarningIfAssemblyVersionDoesNotMatch(Compilation compilation, Type sampleType)
        {
            var runtimeAssemblyName = sampleType.Assembly.GetName();
            var projectAssembly = compilation?.ReferencedAssemblyNames.Where(x => x.Name == runtimeAssemblyName.Name).FirstOrDefault();

            if ((projectAssembly != null) && (projectAssembly.Version != runtimeAssemblyName.Version))
            {
                output.Info($"You are using different version ({projectAssembly.Version}) of {projectAssembly.Name} than version used by runtime ({runtimeAssemblyName.Version})");
            }
        }

        private async Task<List<SyntaxTree>> GetDecoratedSyntaxTreesWithAttribute(IEnumerable<Document> documents, string attributeName)
        {
            var syntaxTrees = new List<SyntaxTree>();

            foreach (var document in documents)
            {
                var syntaxTree = await document.GetSyntaxTreeAsync() as CSharpSyntaxTree;

                // a new way of detecting files destined for compilation: by file extension
                if (document.FilePath.EndsWith(".nt.cs"))
                {
                    syntaxTrees.Add(CSharpSyntaxTree.Create(syntaxTree.GetRoot(), path: syntaxTree.FilePath, encoding : Encoding.UTF8));
                    goto outsideLoop;
                }

                // old way: by presence of the attribute
                var rootNode = await syntaxTree.GetRootAsync();
                var classes = rootNode.DescendantNodes().OfType<ClassDeclarationSyntax>();
                foreach (var @class in classes)
                {
                    foreach (var list in @class.AttributeLists)
                    {
                        foreach (var attribute in list.Attributes)
                        {
                            if (attributeName.StartsWith(attribute.Name.ToString()))
                            {
                                syntaxTrees.Add(CSharpSyntaxTree.Create(syntaxTree.GetRoot(), path: syntaxTree.FilePath, encoding: Encoding.UTF8));
                                goto outsideLoop;
                            }
                        }
                    }
                }
            outsideLoop:;
            }

            return syntaxTrees;
        }       
        private IEnumerable<SyntaxTree> GetSyntaxTrees(IEnumerable<string> configFiles)
        {           
            foreach (var filePath in configFiles)
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var syntaxTree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: filePath);
                    yield return syntaxTree;
                }
            }
        }
        private CSharpCompilation PrepareCompilation(IEnumerable<SyntaxTree> syntaxTreesToCompile, string configurationName)
        {
            var refTypes = new[] {
                typeof(object),
                typeof(Enumerable),
                typeof(IEditorConfig),
                typeof(ICodeModel),
                typeof(ActionFunctions),
                typeof(ISet<>),
                typeof(System.Text.Json.JsonSerializer)

            };
            var references = refTypes.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location)).ToList();

            var netstandardAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "netstandard");
            var netstandardAssembly = netstandardAssemblies.OrderByDescending(x => x.GetName().Version).FirstOrDefault();
            references.Add(MetadataReference.CreateFromFile(netstandardAssembly.Location));

            var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            var runtimeDllPath = Path.Combine(runtimeDir, "System.Runtime.dll");
            references.Add(MetadataReference.CreateFromFile(runtimeDllPath));

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithOptimizationLevel(OptimizationLevel.Debug);
            var compilation = CSharpCompilation.Create($"NTDynamic{configurationName}Assembly", syntaxTrees: syntaxTreesToCompile, references: references, compilationOptions);

            return compilation;
        }

        private Type GetFirstTypeThatImplementsGivenInterface(Assembly assembly, string interfaceName)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterface(interfaceName) != null)
                {
                    return type;
                }
            }
            return null;
        }

        class CacheItem
        {
            public readonly Result Result;
            public readonly long Hash;

            public CacheItem(IEnumerable<string> paths, Result result)
            {
                Result = result;
                Hash = CalulateHash(paths);

            }

            public bool AreTheSame(IEnumerable<string> paths)
            {
                var hash = CalulateHash(paths);
                return hash == this.Hash;
            }

            private long CalulateHash(IEnumerable<string> paths)
            {
                long hash = 0;
                foreach (var path in paths)
                {
                    var dateTime = File.GetLastWriteTime(path).Ticks / 10000000;
                    hash += dateTime;
                    hash %= ((2L << 60) - 1);
                }
                return hash;
            }
        }

        #region
        static UserCodeLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var currentDomain = sender as AppDomain;
            var assemblies = currentDomain.GetAssemblies();
            var assembly = assemblies.FirstOrDefault(x => x.FullName == args.Name);
            return assembly;
        }
        #endregion
    }
}