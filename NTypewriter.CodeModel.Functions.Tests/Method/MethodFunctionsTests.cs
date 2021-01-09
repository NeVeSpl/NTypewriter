using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.CodeModel.Functions.Tests.Method
{
    [TestClass]
    public class MethodFunctionsTests : BaseFixture
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
        public async Task ActionHttpMethod()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Method.ActionHttpMethod }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:get][SomeAsync:put][SomeAsync2:delete]";
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task ActionTypeSentByBody()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Method.ActionTypeSentByBody }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:int][SomeAsync:InputDTO][SomeAsync2:InputDTO]";
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task ActionUrl()
        {
            var template = @"{{- capture result
                                 for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" 
                                     for method in class.Methods }}                       
                                      [{{- method.Name }} : {{- method | Method.ActionUrl }}]
                                   {{- end
                                 end 
                                 end 
                                 Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:WeatherForecast/hkk][SomeAsync:sd][SomeAsync2:WeatherForecast/akacja/${par1}/${par2}/${par3}?par4=${par4}&par5=${par5}]";
            Assert.AreEqual(expected, actual);
        }


    }
}
