using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Type
{
    [TestClass]
    public class TypeRenderingTests : BaseFixture
    {

        [TestMethod]
        public async Task IsEnumerable()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsCollection()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsEnum()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task IsGeneric()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsNullable()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsAnonymousType()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task IsPrimitive()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task BaseType()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task Interfaces()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task AllInterfaces()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task TypeArguments()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Name()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task BareName()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task FullName()
        {
            await RunTestForProperty();
        }



      
    }
}
