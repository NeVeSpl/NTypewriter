using System;
using System.Collections.Generic;
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
        private static long ExecuteCount = 0;
        private static long RenderCount = 0;
        private static string LastRender = null;
        private static string Version = typeof(NTypewriterSourceGenerator).Assembly.GetName().Version.ToString();
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
                 $"// Scriban.Signed : {typeof(Template).Assembly.Location}",             
            };
            
            var allPaths = String.Join("\r\n", paths);
            context.AddSource("Diagnostics.g.cs", allPaths);
        }
        

        public void Execute(GeneratorExecutionContext context)
        {
            Interlocked.Increment(ref ExecuteCount);
            var thisRunId = DateTime.Now.ToString();

            string outputPath = GetOutputPath(context);
            string touchFilePath = Path.Combine(outputPath, ".touch");
            string logFilePath = Path.Combine(outputPath, context.Compilation.AssemblyName + ".ntsg.log");
            string lastTouch = File.GetLastWriteTime(touchFilePath).ToString();

            bool doRender = false;

            lock (Padlock)
            {
                if (lastTouch != LastRender)
                {
                    Interlocked.Increment(ref RenderCount);
                    doRender = true;
                    LastRender = lastTouch;
                }
            }

            var lines = new string[]
            {
                $"NTypewriter.SourceGenerator v{Version}",
                $"total runs : {ExecuteCount}, total renders : {RenderCount}",
                $"touch file : {touchFilePath}",
                $"log file   : {logFilePath}",
                $"this run   : {thisRunId}",
                $"last build : {lastTouch}",
            };
         
            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ExecuteInfo, Location.None, String.Join("\n", lines)));

            if (doRender == false) return;

            try
            {
                var templates = context.AdditionalFiles.Where(x => x.Path.EndsWith(".nt")).Select(x => new TemplateToRender(x.Path, context.Compilation.AssemblyName) { Content = x.GetText().ToString()} ).ToList();
                var userCodePaths = context.Compilation.SyntaxTrees.Where(x => x.FilePath?.EndsWith(".nt.cs") == true).Select(x => x.FilePath).ToList();

                var userCodeProvider = new UserCodeProvider(userCodePaths);
                var userInterfaceOutputWriter = new UserInterfaceOutputWriter();

                var cmd = new RenderTemplatesCommand(null, userCodeProvider, new GeneratedFileReaderWriter(), userInterfaceOutputWriter, null, null, null, null);
                cmd.Execute(context.Compilation, templates).GetAwaiter().GetResult();

                var log = userInterfaceOutputWriter.GetOutput();
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.RenderInfo, Location.None, log));
                File.WriteAllText(logFilePath, log);
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.Exception, Location.None, ex.ToString()));
            }        
        }

        private static string GetOutputPath(GeneratorExecutionContext context)
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir);
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.OutputPath", out var outputPath);
            string fullOutputPath = Path.Combine(outputPath);
            if (Path.IsPathRooted(outputPath) == false)
            {
                fullOutputPath = Path.Combine(projectDir, fullOutputPath);
            }

            return fullOutputPath;
        }
    }
}