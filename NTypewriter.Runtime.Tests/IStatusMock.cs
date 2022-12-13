using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class IStatusMock : IUserInterfaceStatusUpdater
    {
        public Task Update(string text, int progress = 0, int total = 0)
        {
            return Task.CompletedTask;
        }
    }
}
