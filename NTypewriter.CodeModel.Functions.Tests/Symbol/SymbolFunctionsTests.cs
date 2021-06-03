using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.CodeModel.Functions.Tests.Symbols
{
    [TestClass]
    public class SymbolFunctionsTests : BaseFixture
    {
        ICodeModel data;


        [TestInitialize]
        public async Task Initialize()
        {
            var config = new CodeModelConfiguration().FilterByNamespace("Tests.Assets.Referenced");
            data = await CreateCodeModelFromProject(config, "Tests.Assets.Referenced");
        }


        [TestMethod]
        public void HasAttribute()
        {
            var result = data.Classes.Where(x => SymbolFunctions.HasAttribute(x, "DebuggerDisplay"));          
            Assert.AreEqual(1, result.Count());
        }
    }
}