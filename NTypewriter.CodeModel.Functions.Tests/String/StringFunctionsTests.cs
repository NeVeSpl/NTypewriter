using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Functions.Tests.String
{
    [TestClass]
    public class StringFunctionsTests : BaseFixture
    {
        [TestMethod]
        [DataRow("Foo", "foo")]
        [DataRow("FooDoo", "fooDoo")]
        [DataRow("", "")]
        [DataRow("Foo_Doo", "fooDoo")]
        [DataRow("foo_doo", "fooDoo")]
        public void ToCamelCase(string input, string expected)
        {
            var result = StringFunctions.ToCamelCase(input);
            Assert.AreEqual(expected, result);
        }
    }
}
