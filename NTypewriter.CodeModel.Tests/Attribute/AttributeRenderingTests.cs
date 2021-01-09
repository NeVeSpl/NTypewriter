using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Attribute
{
    [TestClass]
    public class AttributeRenderingTests : BaseFixture
    { 
        [TestMethod]
        public async Task FullName()
        {
            await RunTestForProperty();
        }    

        [TestMethod]
        public async Task Name()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Arguments()
        {
            await RunTestForProperty();
        }
    }
}