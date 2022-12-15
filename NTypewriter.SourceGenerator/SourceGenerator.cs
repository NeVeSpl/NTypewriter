using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using NTypewriter.Runtime;
using NTypewriter.SourceGenerator.Adapters;

namespace NTypewriter.SourceGenerator
{
    [Generator]
    public class NTypewriterSourceGenerator : ISourceGenerator
    {
        private static long ExecuteCount = 0;
        private static long RenderCount = 0;
        private static string LastRender;
        private static string Version = typeof(NTypewriterSourceGenerator).Assembly.GetName().Version.ToString();
        private static readonly object Padlock = new Object();

        static NTypewriterSourceGenerator()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver.Resolve;
        }


        public void Initialize(GeneratorInitializationContext context)
        {            

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
                var templates = context.AdditionalFiles.Where(x => x.Path.EndsWith(".nt")).Select(x => new TemplateToRender(x.Path, "") { Content = x.GetText().ToString()} ).ToList();

                var userInterfaceOutputWriter = new UserInterfaceOutputWriter();
                var cmd = new RenderTemplatesCommand(null, new UserCodeProvider(), new GeneratedFileReaderWriter(), userInterfaceOutputWriter, null, null, null, null);
                cmd.Execute(context.Compilation, templates).GetAwaiter().GetResult();
                var log = userInterfaceOutputWriter.sb.ToString();
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.TouchInfo, Location.None, log));
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
            string touchFile = Path.Combine(outputPath);
            if (Path.IsPathRooted(outputPath) == false)
            {
                touchFile = Path.Combine(projectDir, touchFile);
            }

            return touchFile;
        }

    }
}