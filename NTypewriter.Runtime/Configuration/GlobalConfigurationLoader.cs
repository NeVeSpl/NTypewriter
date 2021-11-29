using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Functions;
using NTypewriter.Editor.Config;

namespace NTypewriter.Runtime.Configuration
{
    public class GlobalConfigurationLoader
    {        
        private readonly IOutput output;


        public GlobalConfigurationLoader(IOutput output)
        {
            this.output = output;
        }


        public async Task<IEditorConfig> LoadConfigurationForGivenProject(Solution solution, string projectFilePath)
        {
            output.Info($"Looking for global configuration in {Path.GetFileName(projectFilePath)}");

            var project = solution.Projects.FirstOrDefault(x => x.FilePath == projectFilePath);

            if (project == null)
            {
                return new EditorConfig();
            }

            await CheckUsedAssemblyVersions(project);

            var syntaxTreesToCompile = await GetDecoratedSyntaxTreesWithAttribute(project.Documents, nameof(NTEditorFileAttribute));
            output.Info("Detected files that contain global configuration code : " + String.Join(",", syntaxTreesToCompile.Select(x => Path.GetFileName(x.FilePath))));
            var compilation = PrepareCompilation(syntaxTreesToCompile, "Configuration");

            using (var ms = new MemoryStream())
            {
                var emitResult = compilation.Emit(ms);
                if (emitResult.Success)
                {
                    var configAssembly = Assembly.Load(ms.ToArray());
                    var globalConfigType = GetFirstTypeThatImplementsGivenInterface(configAssembly, nameof(IEditorConfig));

                    if (globalConfigType == null)
                    {
                        output.Info($"Class with global configuration was not found, using default global configuration instead");
                        return new EditorConfig();
                    }

                    output.Info($"Class with global configuration was found : {globalConfigType.Name}");

                    var globalConfigInstance = Activator.CreateInstance(globalConfigType);
                    var config = globalConfigInstance as IEditorConfig;
                    if (config == null)
                    {
                        output.Error($"Class {globalConfigType.Name} does not implement IEditorConfig interface, using default global configuration instead");
                        return new EditorConfig();
                    }

                    config.TypesThatContainCustomFunctions = configAssembly.GetTypes().Where(x => x.IsClass).Where(x => x.CustomAttributes.All(y => y.AttributeType.Name != "CompilerGeneratedAttribute")).ToList();

                    output.Info("Global configuration loaded successfully");
                    return config;
                }
                else
                {
                    var listOfErrors = new List<string>() { "Failed to compile global configuration" };

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
                var syntaxTree = await document.GetSyntaxTreeAsync();

                // a new way of detecting files destined for compilation: by file extension
                if (document.FilePath.EndsWith(".nt.cs"))
                {
                    syntaxTrees.Add(syntaxTree);
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
                                syntaxTrees.Add(syntaxTree);
                                goto outsideLoop;
                            }
                        }
                    }
                }
            outsideLoop:;
            }

            return syntaxTrees;
        }
        private CSharpCompilation PrepareCompilation(List<SyntaxTree> syntaxTreesToCompile, string configurationName)
        {
            var refTypes = new[] {
                typeof(object),
                typeof(Enumerable),
                typeof(NTEditorFileAttribute),
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

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
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


        #region
        static GlobalConfigurationLoader()
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