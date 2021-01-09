using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.SymbolBase
{
    [TestClass]
    public class SymbolBaseRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task Attributes()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task DocComment()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsPublic()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsStatic()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsAbstract()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsArray()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsEvent()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsField()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsMethod()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsProperty()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task ContainingType()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Name()
        {
            await RunTestForProperty();
        }

      

        [TestMethod]
        public async Task FullName()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Namespace()
        {
            await RunTestForProperty();
        }
    }
}