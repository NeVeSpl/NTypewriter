using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
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
        private static readonly ConcurrentDictionary<(string AssemblyName, string OutputDir), GeneratorInfo> AssemblyGeneratorInfo = new ConcurrentDictionary<(string, string), GeneratorInfo>();
        private static readonly string Version = typeof(NTypewriterSourceGenerator).Assembly.GetName().Version.ToString();
        private static readonly object Padlock = new Object();

        static NTypewriterSourceGenerator()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver.Resolve;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization(PostInitializationContext);
        }

        public void PostInitializationContext(GeneratorPostInitializationContext context)
        {
            var paths = new string[]
            {
                 $"// NTypewriterSourceGenerator : {typeof(NTypewriterSourceGenerator).Assembly.Location}",
                 $"// NTypewriter : {typeof(NTypeWriter).Assembly.Location}",
                 $"// NTypewriter.CodeModel : {typeof(ICodeModel).Assembly.Location}",
                 $"// NTypewriter.CodeModel.Functions : {typeof(ActionFunctions).Assembly.Location}",
                 $"// NTypewriter.CodeModel.Roslyn : {typeof(CodeModelConfiguration).Assembly.Location}",
                 $"// NTypewriter.Editor.Config : {typeof(EditorConfig).Assembly.Location}",
                 $"// NTypewriter.Runtime : {typeof(RenderTemplatesCommand).Assembly.Location}",
                 $"// Scriban.Signed : {typeof(Template).Assembly.Location}"
            };

            var allPaths = String.Join("\r\n", paths);
            context.AddSource("Diagnostics.g.cs", allPaths);
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var info = GetGeneratorInfo(context);

            Interlocked.Increment(ref info.ExecuteCount);
            info.LastTouch = File.GetLastWriteTime(info.TouchFilePath);
            var thisRunId = DateTime.Now.ToString();

            bool doRender = false;

            lock (Padlock)
            {
                if (DateTime.Compare(info.LastTouch, info.LastRender) != 0)
                {
                    Interlocked.Increment(ref info.RenderCount);
                    doRender = true;
                    info.LastRender = info.LastTouch;
                }
            }

            var lines = new string[]
            {
                $"NTypewriter.SourceGenerator v{Version}",
                $"total runs : {info.ExecuteCount}, total renders : {info.RenderCount}",
                $"touch file : {info.TouchFilePath}",
                $"log file   : {info.LogFilePath}",
                $"this run   : {thisRunId}",
                $"last build : {info.LastTouch}"
            };

            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ExecuteInfo, Location.None, String.Join("\n", lines)));

            if (doRender == false) return;

            try
            {
                var templates = context.AdditionalFiles.Where(x => x.Path.EndsWith(".nt")).Select(x => new TemplateToRender(x.Path, context.Compilation.AssemblyName) { Content = x.GetText().ToString() }).ToList();
                var userCodePaths = context.Compilation.SyntaxTrees.Where(x => x.FilePath?.EndsWith(".nt.cs") == true).Select(x => x.FilePath).ToList();

                var userCodeProvider = new UserCodeProvider(userCodePaths);
                var userInterfaceOutputWriter = new UserInterfaceOutputWriter();

                var cmd = new RenderTemplatesCommand(null, userCodeProvider, new GeneratedFileReaderWriter(context, info.ProjectDir), userInterfaceOutputWriter, null, null, null, null);
                cmd.Execute(context.Compilation, templates).GetAwaiter().GetResult();

                var log = userInterfaceOutputWriter.GetOutput();
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.RenderInfo, Location.None, log));
                File.WriteAllText(info.LogFilePath, log);
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.Exception, Location.None, ex.ToString()));
            }
        }

        private static GeneratorInfo GetGeneratorInfo(GeneratorExecutionContext context)
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.OutputPath", out var outputPath);
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir);
            var assemblyName = context.Compilation.AssemblyName;
            var outputDir = GetOutputDir(outputPath, projectDir);

            return AssemblyGeneratorInfo.GetOrAdd((assemblyName, outputDir), _ => new GeneratorInfo(assemblyName, projectDir, outputDir));
        }

        private static string GetOutputDir(string outputPath, string projectDir)
        {
            string fullOutputPath = Path.Combine(outputPath);
            if (Path.IsPathRooted(outputPath) == false)
            {
                fullOutputPath = Path.Combine(projectDir, fullOutputPath);
            }

            return fullOutputPath;
        }

        private sealed class GeneratorInfo
        {
            public GeneratorInfo(string assemblyName, string projectDir, string outputDir)
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