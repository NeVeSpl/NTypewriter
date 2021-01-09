using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Class
{
    [TestClass]
    public class ClassRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task BaseClass()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Constructors()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Events()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Fields()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Methods()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task NestedClasses()
        {
            await RunTestForProperty();
        }
    }
}