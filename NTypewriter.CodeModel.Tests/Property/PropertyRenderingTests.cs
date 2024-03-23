using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Property
{
    [TestClass]
    public class PropertyRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task IsReadOnly()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsWriteOnly()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task GetMethod_IsPublic()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task SetMethod_IsPublic()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Type()
        {
            await RunTestForProperty();
        }
    }
}
