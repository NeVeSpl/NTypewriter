using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Functions.Tests.Types
{
    [TestClass]
    public class TypesFunctionsTests : BaseFixture
    {
        NTypewriter.CodeModel.ICodeModel data;

        [TestInitialize]
        public async Task Initialize()
        {
            data = await CreateCodeModelFromProject(null, "Tests.Assets.Referenced");
        }


        [TestMethod]
        public void ThatImplement()
        {
            var result = TypesFunctions.ThatImplement(data.Classes, "ICanBeEaten");
            Assert.AreEqual("AppleDTO", result.First().Name);

        }

        [TestMethod]
        public void ThatInheritFrom()
        {
            var result = TypesFunctions.ThatInheritFrom(data.Classes, "Fruit");
            Assert.AreEqual("Apple", result.First().Name);
        }
    }
}
