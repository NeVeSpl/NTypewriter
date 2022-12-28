using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NTypewriter.Online.Adapters;
using NTypewriter.Runtime;

namespace NTypewriter.Online
{
    public class Runner
    {
        private static IEnumerable<MetadataReference> References;

        public async Task Initialize(string baseUri)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            var references = new List<MetadataReference>();
            foreach (var assembly in assemblies.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location)))
            {                
                Stream stream = await client.GetStreamAsync($"_framework/_bin/{assembly.Location}");
                references.Add(MetadataReference.CreateFromStream(stream));
                Debug.WriteLine($"=> MetadataReference for : {assembly.Location}");
            }

            References = references;
        }     


        public async Task<Result> RunAsync(string inputCode, string template, CancellationToken cancellationToken)
        { 
            var userCodeProvider = new UserCodeProvider();
            var templateToRender = new TemplateToRender("template.nt", "NTypewriter.Online.Demo") { Content = template };

            var generatorTree = CSharpSyntaxTree.ParseText(inputCode, new CSharpParseOptions(kind: SourceCodeKind.Regular), "InputCode.cs", cancellationToken: cancellationToken);
            var generatorCompilation = CSharpCompilation.Create("NTypewriter.Online.Demo", new[] { generatorTree }, References, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
                   
            foreach (var diagnostic in generatorCompilation.GetDiagnostics())
            {
                if (diagnostic.Severity == DiagnosticSeverity.Error)
                {
                    Debug.WriteLine($"=> {diagnostic}");
                }
            }

            var generatedFileReaderWriter = new GeneratedFileReaderWriter();
            var userIO = new UserInterfaceOutputWriter();

            try
            {
                var cmd = new RenderTemplatesCommand(null, userCodeProvider, generatedFileReaderWriter, userIO, null, null, null, null);
                await cmd.Execute(generatorCompilation, new[] { templateToRender });
            } 
            catch (Exception ex)
            {
                Debug.WriteLine($"> {ex}");
            }
            return new Result(generatedFileReaderWriter.GetOutput(), userIO.GetOutput());
        }


        public class Result
        {
            public string GeneratedFiles { get; set; }  
            public string NTypewriterOutput { get; set; }

            public Result(string generatedFiles, string nTypewriterOutput)
            {
                GeneratedFiles = generatedFiles;
                NTypewriterOutput = nTypewriterOutput;
            }
        }
    }
}