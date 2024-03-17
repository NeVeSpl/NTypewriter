using System;
using System.IO;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace NTypewriter.SourceGenerator.Incremental
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(PostInitialization);

            
            IncrementalValuesProvider<AdditionalText> ntFiles = context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".nt"));
            var namesAndContents = ntFiles.Select(NTFileHasChangedSelect);
            
            context.RegisterSourceOutput(namesAndContents, NTFileHasChanged);
        }

        private NTFileHasChangedArgs NTFileHasChangedSelect(AdditionalText text, CancellationToken cancellationToken)
        {
            var name = Path.GetFileNameWithoutExtension(text.Path);
            var content = text.GetText(cancellationToken)!.ToString();

            return new NTFileHasChangedArgs(name, content);
        }

        private void NTFileHasChanged(SourceProductionContext sourceProductionContext, NTFileHasChangedArgs args)
        {
            sourceProductionContext.AddSource($"NTFileHasChanged_{args.Name}.cs", $"// {DateTime.Now.ToString()}");
        }




        private void PostInitialization(IncrementalGeneratorPostInitializationContext context)
        {
            var postInitializationRaport = "// Hello world";
            context.AddSource("diagnostics-initialization.g.cs", postInitializationRaport);
        }






        private record NTFileHasChangedArgs
        {
            public string Name { get; set; }
            public string Content { get; set; }


            public NTFileHasChangedArgs(string name, string content)
            {
                Name = name;
                Content = content;
            }     
        }
    }
}
