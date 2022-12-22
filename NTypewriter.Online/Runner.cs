using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using NTypewriter.Online.Adapters;
using NTypewriter.Runtime;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Net.Http;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace NTypewriter.Online
{
    public class Runner
    {
        private static readonly Dictionary<string, MetadataReference> s_references = new Dictionary<string, MetadataReference>();
        private readonly string _baseUri;




        public Runner(NavigationManager navigationManager)
        {
            _baseUri = navigationManager.BaseUri;
        }



        internal async Task RunAsync(string code, string template, CancellationToken cancellationToken)
        {
            await UpdateReferences(_baseUri);
            var userCodeProvider = new UserCodeProvider();

            var ttr = new TemplateToRender("test.nt", "NTypewriter.Online.Demo") { Content= template };


            var generatorTree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(kind: SourceCodeKind.Regular), "InputCode.cs", cancellationToken: cancellationToken);

            var generatorCompilation = CSharpCompilation.Create("InputCode", new[] { generatorTree }, s_references.Values, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));



            foreach (var d in generatorCompilation.GetDiagnostics())
            {
                if (d.Severity == DiagnosticSeverity.Error)
                {
                    Debug.WriteLine(d.ToString());
                }
            }


            var cmd = new RenderTemplatesCommand(null, userCodeProvider, new GeneratedFileReaderWriter(), null, null, null, null, null);
            await cmd.Execute(generatorCompilation, new[] { ttr });
        }


        private async Task UpdateReferences(string baseUri)
        {
            Assembly[] refs = AppDomain.CurrentDomain.GetAssemblies();
            var client = new Lazy<HttpClient>(() => new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            });

            foreach (Assembly reference in refs.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location)))
            {
                if (!s_references.ContainsKey(reference.Location))
                {
                    Stream stream = await client.Value.GetStreamAsync($"_framework/_bin/{reference.Location}");
                    s_references.Add(reference.Location, MetadataReference.CreateFromStream(stream));
                }

            }
            Debug.WriteLine(s_references.Values.Count);
        }
    }
}
