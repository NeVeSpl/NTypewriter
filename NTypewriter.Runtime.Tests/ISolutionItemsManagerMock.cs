using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime.Tests
{
    internal class ISolutionItemsManagerMock : ISolutionItemsManager
    {
        public Task UpdateSolution(string templateFilePath, IEnumerable<string> createdFiles)
        {
            return Task.CompletedTask;  
        }
    }
}
