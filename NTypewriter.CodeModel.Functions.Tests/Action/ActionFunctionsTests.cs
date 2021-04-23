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
    public class ActionFunctionsTests : BaseFixture
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
        public async Task HttpMethod()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Action.HttpMethod }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = RemoveWhitespace(
                           @"[GetData:get]
                             [SomeAsync:put]
                             [SomeAsync2:delete]
                             [ActionWithEnumParam:post]");
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task BodyParameter()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Action.BodyParameter }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = RemoveWhitespace(
                              @"[GetData:intbody]
                                [SomeAsync:InputDTObody]
                                [SomeAsync2:InputDTObody]
                                [ActionWithEnumParam:]");
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task Url()
        {
            var template = @"{{- capture result
                                 for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" 
                                     for method in class.Methods }}                       
                                      [{{- method.Name }} : {{- method | Action.Url }}]
                                   {{- end
                                 end 
                                 end 
                                 Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = RemoveWhitespace(
                           @"[GetData:WeatherForecast/hkk]
                             [SomeAsync:sd?page=${pagg.page}&limit=${pagg.limit}]
                             [SomeAsync2:WeatherForecast/akacja/${par1}/${par2}/${par3}?par4=${par4}&par5=${par5}]
                             [ActionWithEnumParam:WeatherForecast?numbers=${numbers}&optional=${optional}&date=${date}]");
                          
            Assert.AreEqual(expected, actual);
        }


    }
}
