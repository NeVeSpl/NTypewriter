using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel.Roslyn;
using Shouldly;

namespace NTypewriter.CodeModel.Tests
{
    public class BaseFixture
    {
        public async Task RunTestForProperty([CallerMemberName] string propertyName = "", [CallerFilePath] string filePath = "")
        {
            var splittedPath = filePath.Split('\\');
          
            var typeName = splittedPath[^2];
            var resources = LoadResources(typeName, propertyName);
            var dataModel = await CreateCodeModel(new CodeModelConfiguration().FilterByNamespace("NTypewriter.Tests.CodeModel"), resources.inputCode);
            var result = await NTypeWriter.Render(resources.template, dataModel, new Configuration());
            var finalResult = result.Items.First().Content;            

            finalResult.ShouldBe(resources.expectedResult);
        }

        private (string template, string expectedResult, string inputCode) LoadResources(string typeName, string propertyName)
        {
            var path = $"{typeName}.{propertyName}";
            string template = LoadResource(path, "inputTemplate.tsnt");
            template = template.Replace("#TypeName#", typeName);
            template = template.Replace("#PropertyName#", propertyName);
            string expectedResult = LoadResource(path, "expectedResult.txt");
            string inputCode = LoadResource(path, "inputCode.cs");
            return (template, expectedResult, inputCode);
        }
        private string LoadResource(string folderWithData, string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"NTypewriter.CodeModel.Tests.{folderWithData}.{fileName}";

            string result = String.Empty;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream is not null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            return result;
        }


        private async Task<NTypewriter.CodeModel.Roslyn.CodeModel> CreateCodeModel(CodeModelConfiguration config, params string[] code)
        {
            var project = CreateRoslynProjectFromCode(code);
            var compilation = await project.GetCompilationAsync();
            var codeModel = new NTypewriter.CodeModel.Roslyn.CodeModel(compilation, config);
            return codeModel;
        }
        private Project CreateRoslynProjectFromCode(params string[] code)
        {
            var references = ImmutableList.Create<MetadataReference>(
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location));

            var project = new AdhocWorkspace()
                .AddProject("TestProject", LanguageNames.CSharp)
                .AddMetadataReferences(references);

            for (int i = 0; i < code.Length; i++)
            {
                string item = code[i];
                project = project.AddDocument($"TestDocument_{i}", item).Project;
            }

            return project;
        }

    }
}