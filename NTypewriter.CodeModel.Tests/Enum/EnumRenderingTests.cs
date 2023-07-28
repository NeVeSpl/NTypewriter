using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Enum
{
    [TestClass]
    public class EnumRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task UnderlyingType()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Values()
        {
            await RunTestForProperty();
        }
    }
}