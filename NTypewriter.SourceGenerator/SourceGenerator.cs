using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.SourceGenerator
{
    [Generator]
    public class NTypewriterSourceGenerator : ISourceGenerator
    {
        private static long ExecuteCount = 0;
        private static string Version = typeof(NTypewriterSourceGenerator).Assembly.GetName().Version.ToString();

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
            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ExecuteInfo, Location.None, $"version : {Version}, total runs : {ExecuteCount}, last run : {DateTime.Now}"));           

            try
            {
                ProofOfConcept(context.Compilation, context.AdditionalFiles);
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.Exception, Location.None, ex.ToString()));
            }

            //context.AddSource($"NTypewriterSourceGenerator.g.cs", sb.ToString());            
        }

        
        private void ProofOfConcept(Compilation compilation, ImmutableArray<AdditionalText> additionalFiles)
        {            
            var codeModelConfiguration = new CodeModelConfiguration() { OmitSymbolsFromReferencedAssemblies = true };
            var codeModel = new NTypewriter.CodeModel.Roslyn.CodeModel(compilation, codeModelConfiguration);

            var template = additionalFiles.FirstOrDefault(x => x.Path.EndsWith(".nt"));
            if (template == null) return;
            
            var ntypewriterConfig = new Configuration();
            NTypeWriter.Render(template.GetText().ToString(), codeModel, ntypewriterConfig).ContinueWith(x => ParseResult(x.Result, template.Path));

        }

        private void ParseResult(Result result, string templateFilePath)
        {
            if (!result.HasErrors)
            {
                var renderedItem = result.Items.First();
                var rootDirectory = Path.GetDirectoryName(templateFilePath);
                var path = Path.Combine(rootDirectory, renderedItem.Name);
                File.WriteAllText(path, renderedItem.Content);
            }
            else
            {
                foreach (var msg in result.Messages)
                {
                    Trace.WriteLine(msg.Message);
                }
            }
        }
    }
}