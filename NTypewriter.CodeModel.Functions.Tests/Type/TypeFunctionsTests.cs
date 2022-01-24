using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.CodeModel.Functions.Tests.Type
{
    [TestClass]
    public class TypeFunctionsTests : BaseFixture
    {
        ICodeModel data;


        [TestInitialize]
        public async Task Initialize()
        {
            var config = new CodeModelConfiguration().FilterByNamespace("NTypewriter.CodeModel.Functions.Tests.Type");
            data = await CreateCodeModelFromProject(config, "NTypewriter.CodeModel.Functions.Tests");
        }


        [TestMethod]
        public async Task AllReferencedTypes()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""AllReferencedTypes""
                                         for type in class | Type.AllReferencedTypes SearchIn.All
                                             type.Name + ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
PropertyType
MethodReturnType
ParameterType
MethodReturnType2
FieldType
EnumType
EnumerableType
GenericType<int>
ArrayType
BaseType
";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }


        [TestMethod]
        public async Task ToTypeScriptType_Simple()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Simple""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
boolean
number
number | null
string
string | null
MyEnum
MyEnum | null
Promise<number>
number
any
string
string | null
string
string | null";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }

        [TestMethod]
        public async Task ToTypeScriptType_Complex()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Complex""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
number[]
MyGeneric<number>
number[]
number[]
MyGeneric<number | null>
(number | null)[]
(number | null)[]
MyGeneric<number | null> | null
(number | null)[] | null
(number | null)[] | null
{ [key: string]: number }
{ [key in keyof typeof MyEnum]?: string }";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }

        [TestMethod]
        public async Task ToTypeScriptType_Simple_CustomNullableType()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Simple""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType ""undefined"" | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
boolean
number
number | undefined
string
string | undefined
MyEnum
MyEnum | undefined
Promise<number>
number
any
string
string | undefined
string
string | undefined";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }

        [TestMethod]
        public async Task ToTypeScriptType_Complex_CustomNullableType()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Complex""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType ""undefined"" | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
number[]
MyGeneric<number>
number[]
number[]
MyGeneric<number | undefined>
(number | undefined)[]
(number | undefined)[]
MyGeneric<number | undefined> | undefined
(number | undefined)[] | undefined
(number | undefined)[] | undefined
{ [key: string]: number }
{ [key in keyof typeof MyEnum]?: string }";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }

        [TestMethod]
        public async Task ToTypeScriptType_Simple_EmptyNullableType()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Simple""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType """" | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
boolean
number
number
string
string
MyEnum
MyEnum
Promise<number>
number
any
string
string
string
string";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }

        [TestMethod]
        public async Task ToTypeScriptType_Complex_EmptyNullableType()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptType_Complex""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptType """" | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
number[]
MyGeneric<number>
number[]
number[]
MyGeneric<number>
number[]
number[]
MyGeneric<number>
number[]
number[]
{ [key: string]: number }
{ [key in keyof typeof MyEnum]?: string }";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }


        [TestMethod]
        public async Task ToTypeScriptDefault()
        {
            var template = @"{{- capture output
                                     for class in data.Classes | Symbols.WhereNameStartsWith ""ToTypeScriptDefault""
                                         for field in class.Fields
                                             field.Type | Type.ToTypeScriptDefault | String.Append ""\r\n""
                                         end
                                      end
                                  end
                                  Save output ""Some name"" }}
                            ";
            var result = await NTypeWriter.Render(template, data, null);
            var actual = result.Items.First().Content;
            var expected = @"
false
null
0
null
0
""""
null
0
null
[]
{}
""""
null";
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }
    }
}
