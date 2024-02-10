using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
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
            projectContext.LastTouchTime = File.GetLastWriteTime(projectContext.TouchFilePath);
            projectContext.LastNTFileEditTime = GetLastEditTime(context.AdditionalFiles);

            bool doRender = false;

            lock (Padlock)
            { 
                if ((projectContext.LastTouchTime > projectContext.LastRenderTime) || (projectContext.LastNTFileEditTime > projectContext.LastRenderTime))
                {
                    Interlocked.Increment(ref projectContext.RenderCount);
                    projectContext.LastRenderTime = new DateTime(Math.Max(projectContext.LastTouchTime.Ticks, projectContext.LastNTFileEditTime.Ticks));                    
                    doRender = true;                    
                }
            }

            var report = projectContext.PrepareRaport();
            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ExecuteInfo, Location.None, report));
            context.AddSource("diagnostics-sg-last-run.g.cs", report);

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
        private static DateTime GetLastEditTime(ImmutableArray<AdditionalText> additionalFiles)
        {
            try
            {
                var lastEdit = additionalFiles.Max(x => File.GetLastWriteTime(x.Path));
                return lastEdit;
            }
            catch 
            {
            }
            return DateTime.MinValue;
        }


        private sealed class ProjectContext(string assemblyName, string projectDir, string buildOutputDir)
        {
            public long ExecuteCount;
            public long RenderCount;

            public string AssemblyName => assemblyName;
            public string ProjectDir => projectDir;
            public string BuildOutputDir => buildOutputDir;            
            public DateTime LastRenderTime { get; set; }
            public string TouchFilePath => Path.Combine(buildOutputDir, ".touch");
            public DateTime LastTouchTime { get; set; }
            public DateTime LastNTFileEditTime { get; set; }
            public string LogFilePath => Path.Combine(buildOutputDir, assemblyName + ".ntsg.log");


            public string PrepareRaport()
            {
                var raport =
                $"""
                    // NTypewriter.SourceGenerator v{typeof(ProjectContext).Assembly.GetName().Version.ToString()}
                    // total runs     : {ExecuteCount}, total renders : {RenderCount}
                    // touch file     : {TouchFilePath}
                    // log file       : {LogFilePath}                    
                    // last build     : {LastTouchTime}
                    // last *.nt edit : {LastNTFileEditTime}
                    // last render    : {LastRenderTime}
                """;
                return raport;
            }
        }
    }
}