using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Delegate
{
    [TestClass]
    public class DelegateRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task ReturnType()
        {
            await RunTestForProperty();
        }
    }
}