using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.AttributeArgument
{
    [TestClass]
    public class AttributeArgumentRenderingTests : BaseFixture
    { 
     

        [TestMethod]
        public async Task Value()
        {
            await RunTestForProperty();
        }
    }
}