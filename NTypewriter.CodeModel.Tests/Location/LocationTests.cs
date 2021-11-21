using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NTypewriter.CodeModel.Tests.Location
{
    [TestClass]
    public class LocationTests : BaseFixture
    {
        [TestMethod]
        public async Task Path()
        {
            await RunTestForProperty();
        }

        [TestMethod]
        public async Task StartLinePosition()
        {
            await RunTestForProperty();
        }
    }
}
