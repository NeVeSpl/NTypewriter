using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;
using Shouldly;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Action
{
    [TestClass]
    public partial class ActionFunctionsTests : BaseFixture
    {
        ICodeModel data;
        Configuration settings;

        [TestInitialize]
        public async Task Initialize()
        {
            var config = new CodeModelConfiguration().FilterByNamespace("Tests.Assets.WebApi");
            data = await CreateCodeModelFromProject(config, "Tests.Assets.WebApi");
            settings = new Configuration();
        }

        [TestMethod]
        public async Task BodyParameter()
        {          
            await RunTestForFunction();
        }

        [TestMethod]
        public async Task HttpMethod()
        {
            await RunTestForFunction();
        }

        [TestMethod]
        public async Task Parameters()
        {
            await RunTestForFunction();
        }

        [TestMethod]
        public async Task ReturnType()
        {
            await RunTestForFunction();
        }

        [TestMethod]
        public async Task Url()
        {
            await RunTestForFunction();
        }


        private async Task RunTestForFunction([CallerMemberName] string propertyName = "", [CallerFilePath] string filePath = "")
        {
            var splittedPath = filePath.Split('\\');
            var typeName = splittedPath[^2];

            var (template, expected) = LoadResources(typeName, propertyName);
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = result.Items.First().Content;
            actual.ShouldBe(expected);

        }
    }
}