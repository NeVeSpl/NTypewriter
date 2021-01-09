using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Event
{
    [TestClass]
    public class EventRenderingTests : BaseFixture
    {

        [TestMethod]
        public async Task Type()
        {
            await RunTestForProperty();
        }
    }
}
