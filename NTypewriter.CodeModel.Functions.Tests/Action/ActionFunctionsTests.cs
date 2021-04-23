﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;
using System.Linq;
using System.Threading.Tasks;

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

            var expected = "[GetData:get][GetDataNoBody:get][SomeAsync:put][SomeAsync2:delete]";
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

            var expected = "[GetData:intbody][GetDataNoBody:][SomeAsync:InputDTObody][SomeAsync2:InputDTObody]";
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public async Task Parameters()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Action.Parameters }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:[intbody]][GetDataNoBody:[inturl]][SomeAsync:[InputDTObody,Pagginationpagg]][SomeAsync2:[intpar3,InputDTObody,doublepar1,boolpar2,intpar4,intpar5]]";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Parameters_without_body_parameters_true()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Action.Parameters true }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:[]][GetDataNoBody:[inturl]][SomeAsync:[Pagginationpagg]][SomeAsync2:[intpar3,doublepar1,boolpar2,intpar4,intpar5]]";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Parameters_without_body_parameters_false()
        {
            var template = @"{{- capture result }}
                                {{- for class in data.Classes | Types.ThatInheritFrom ""ControllerBase"" }}
                                   {{- for method in class.Methods }}                            
                                      [{{- method.Name }} : {{- method | Action.Parameters false }}]
                                   {{- end }}
                                {{- end }}
                             {{- end }}
                             {{- Save result ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, settings);
            var actual = RemoveWhitespace(result.Items.First().Content);

            var expected = "[GetData:[intbody]][GetDataNoBody:[inturl]][SomeAsync:[InputDTObody,Pagginationpagg]][SomeAsync2:[intpar3,InputDTObody,doublepar1,boolpar2,intpar4,intpar5]]";
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

            var expected = "[GetData:WeatherForecast/hkk][GetDataNoBody:WeatherForecast/hkk/${url}][SomeAsync:sd?page=${pagg.page}&limit=${pagg.limit}][SomeAsync2:WeatherForecast/akacja/${par1}/${par2}/${par3}?par4=${par4}&par5=${par5}]";
                          
            Assert.AreEqual(expected, actual);
        }


    }
}
