using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Struct
{
    [TestClass]
    public class StructRenderingTests : BaseFixture    
    {
        [TestMethod]
        public async Task Name()
        {
            await RunTestForProperty();
        }
    }
}