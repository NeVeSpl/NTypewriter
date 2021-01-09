using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.CodeModel.Functions.Tests.Custom
{
    [TestClass]
    public class CustomFunctionsTests : BaseFixture
    {

        [TestMethod]
        public async Task UserCanAddCustomFunctions()
        {
            var template = "{{- capture output; Custom.MyFunction;  end; Save output \"result\" }}";
            var expectedResult = "Hello World!";
            var codeModelConfig = new CodeModelConfiguration();
            var dataProvider = await CreateCodeModelFromProject(codeModelConfig, "Tests.Assets.WebApi");
            var settings = new Configuration().AddCustomFunctions(typeof(MyCustomFunctions));
            var result = await NTypeWriter.Render(template, dataProvider, settings);
            var finalResult = result.Items.First().Content;

            Assert.AreEqual(expectedResult, finalResult);
        }



        public static class MyCustomFunctions
        {
            public static string MyFunction()
            {
                return "Hello World!";
            }
        }

    }
}
