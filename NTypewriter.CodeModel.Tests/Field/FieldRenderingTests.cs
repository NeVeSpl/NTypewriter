using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Field
{
    [TestClass]
    public class FieldRenderingTests : BaseFixture
    {

        [TestMethod]
        public async Task Type()
        {
            await RunTestForProperty();
        }


        [TestMethod]
        public async Task IsConst()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task IsReadOnly()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task HasConstantValue()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task ConstantValue()
        {
            await RunTestForProperty();
        }
    }
}
