using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Functions;
using NTypewriter.CodeModel.Roslyn;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime;
using NTypewriter.SourceGenerator.Adapters;
using Scriban;

namespace NTypewriter.SourceGenerator
{
    [Generator]
    public class NTypewriterSourceGenerator : ISourceGenerator
    {
        private static readonly ConcurrentDictionary<string, ProjectContext> ProjectContexts = new();
        private static readonly string Version = typeof(NTypewriterSourceGenerator).Assembly.GetName().Version.ToString();
        private static readonly object Padlock = new();


        static NTypewriterSourceGenerator()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver.Resolve;
        }


        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization(PostInitialization);
        }
        private void PostInitialization(GeneratorPostInitializationContext context)
        {
            Type[] markingTypes =
            [
                typeof(NTypewriterSourceGenerator),
                typeof(NTypeWriter),
                typeof(ICodeModel),
                typeof(ActionFunctions),
                typeof(CodeModelConfiguration),
                typeof(EditorConfig),
                typeof(RenderTemplatesCommand),
                typeof(Template),
                typeof(JsonSerializer),
                typeof(Regex),
            ];

            var assembliesInfoLines = markingTypes.Select(x => $"// {x.Assembly.GetName().Name, -32} {x.Assembly.GetName().Version}  {x.Assembly.Location}");   
            var output = String.Join("\r\n", assembliesInfoLines);

            context.AddSource("diagnostics-initialization.g.cs", output);            
        }


        public void Execute(GeneratorExecutionContext context)
        {
            var projectContext = GetProjectContext(context);

            Interlocked.Increment(ref projectContext.ExecuteCount);
            projectContext.LastTouch = File.GetLastWriteTime(projectContext.TouchFilePath);
            var thisRunId = DateTime.Now.ToString();

            bool doRender = false;

            lock (Padlock)
            {
                if (DateTime.Compare(projectContext.LastTouch, projectContext.LastRender) != 0)
                {
                    Interlocked.Increment(ref projectContext.RenderCount);
                    doRender = true;
                    projectContext.LastRender = projectContext.LastTouch;
                }
            }

            var lines = new string[]
            {
                $"NTypewriter.SourceGenerator v{Version}",
                $"total runs : {projectContext.ExecuteCount}, total renders : {projectContext.RenderCount}",
                $"touch file : {projectContext.TouchFilePath}",
                $"log file   : {projectContext.LogFilePath}",
                $"this run   : {thisRunId}",
                $"last build : {projectContext.LastTouch}"
            };

            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ExecuteInfo, Location.None, String.Join("\n", lines)));

            if (doRender == false) return;

            try
            {
                var templates = context.AdditionalFiles.Where(x => x.Path.EndsWith(".nt")).Select(x => new TemplateToRender(x.Path, context.Compilation.AssemblyName) {Content = x.GetText().ToString()}).ToList();
                var userCodePaths = context.Compilation.SyntaxTrees.Where(x => x.FilePath?.EndsWith(".nt.cs") == true).Select(x => x.FilePath).ToList();

                var userCodeProvider = new UserCodeProvider(userCodePaths);
                var userInterfaceOutputWriter = new UserInterfaceOutputWriter();

                var cmd = new RenderTemplatesCommand(null, userCodeProvider, new GeneratedFileReaderWriter(context, projectContext.ProjectDir), userInterfaceOutputWriter, null, null, null, null);
                cmd.Execute(context.Compilation, templates).GetAwaiter().GetResult();

                var log = userInterfaceOutputWriter.GetOutput();
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.RenderInfo, Location.None, log));
                File.WriteAllText(projectContext.LogFilePath, log);
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.Exception, Location.None, ex.ToString()));
            }
        }

        private static ProjectContext GetProjectContext(GeneratorExecutionContext context)        
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir);
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.OutputPath", out var buildOutputPath);
                     
            var assemblyName = context.Compilation.AssemblyName;
            var outputDir = GetOutputDir(projectDir, buildOutputPath);

            if (string.IsNullOrEmpty(outputDir))
            {
                throw new ArgumentException($"Could not determine output directory for {assemblyName} from parameters : build_property.ProjectDir = {projectDir}, build_property.OutputPath = {buildOutputPath}");
            }

            var id = Path.Combine(outputDir, assemblyName);

            return ProjectContexts.GetOrAdd(id, _ => new ProjectContext(assemblyName, projectDir, outputDir));
        }

        private static string GetOutputDir(string projectDir, string buildOutputPath)
        {             
            if (Path.IsPathRooted(buildOutputPath))
            {
                return buildOutputPath;
            }

            if (!string.IsNullOrEmpty(projectDir) && !string.IsNullOrEmpty(buildOutputPath))
            {
                return Path.Combine(projectDir, buildOutputPath);
            }

            return projectDir;
        }

        private sealed class ProjectContext
        {
            public ProjectContext(string assemblyName, string projectDir, string outputDir)
            {
                (AssemblyName, ProjectDir, OutputDir) = (assemblyName, projectDir, outputDir);
            }

            public string AssemblyName;
            public string ProjectDir;
            public string OutputDir;
            public long ExecuteCount;
            public long RenderCount;
            public DateTime LastRender;
            public string TouchFilePath => Path.Combine(OutputDir, ".touch");
            public DateTime LastTouch;
            public string LogFilePath => Path.Combine(OutputDir, AssemblyName + ".ntsg.log");
        }
    }
}