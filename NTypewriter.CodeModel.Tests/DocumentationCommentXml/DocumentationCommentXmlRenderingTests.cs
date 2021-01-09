using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.DocumentationCommentXml
{
    [TestClass]
    public class DocumentationCommentXmlRenderingTests : BaseFixture
    {
        [TestMethod]
        public async Task Summary()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Returns()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task Params()
        {
            await RunTestForProperty();
        }
    }
}