using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.NamedType
{
    [TestClass]
    public class NamedTypeRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task TypeParameters()
        {
            await RunTestForProperty();
        }
    }
}