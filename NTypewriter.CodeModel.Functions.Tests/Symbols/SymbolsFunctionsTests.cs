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
    public class SymbolsFunctionsTests : BaseFixture
    {
        ICodeModel data;
     

        [TestInitialize]
        public async Task Initialize()
        {
            var config = new CodeModelConfiguration().FilterByNamespace("Tests.Assets.Referenced");       
            data = await CreateCodeModelFromProject(config, "Tests.Assets.Referenced");          
        }


        [TestMethod]
        public void WhereNamespaceStartsWith()
        {
            var result = SymbolsFunctions.WhereNamespaceStartsWith(data.Classes, "Tests.Assets.Referenced.Domain");
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void WhereNamespaceMatches()
        {
            var result = SymbolsFunctions.WhereNamespaceMatches(data.Classes, @"Tests\.Assets.*\.DTO");
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void WhereNameStartsWith()
        {
            var result = SymbolsFunctions.WhereNameStartsWith(data.Classes, "Fru");
            Assert.AreEqual("Fruit", result.First().Name);
        }

        [TestMethod]
        public void WhereNameEndsWith()
        {
            var result = SymbolsFunctions.WhereNameEndsWith(data.Classes, "Fruit");
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void WhereNameMatches()
        {
            var result = SymbolsFunctions.WhereNameMatches(data.Interfaces, @".+Eaten");
            Assert.AreEqual("ICanBeEaten", result.First().Name);
        }

        [TestMethod]
        public void ThatHaveAttribute()
        {
            var result = SymbolsFunctions.ThatHaveAttribute(data.Classes, "DebuggerDisplay");
            Assert.AreEqual("OrangeDTO", result.First().Name);
        }

        [TestMethod]
        public void ThatArePublic()
        {
            var result = SymbolsFunctions.ThatArePublic(data.Classes);
            Assert.AreEqual(3, result.Count());
        }
    }
}