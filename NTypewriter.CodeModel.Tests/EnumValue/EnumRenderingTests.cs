using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Enum
{
    [TestClass]
    public class EnumValueRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task Attributes()
        {
            await RunTestForProperty();
        }
    }
}