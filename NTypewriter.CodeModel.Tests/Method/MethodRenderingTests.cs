using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Method
{
    [TestClass]
    public class MethodRenderingTests : BaseFixture
    {

        [TestMethod]
        public async Task ReturnType()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsGeneric()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsAsync()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task TypeParameters()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Parameters()
        {
            await RunTestForProperty();
        }



        
    }
}
