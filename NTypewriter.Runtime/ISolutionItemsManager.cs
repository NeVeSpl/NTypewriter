using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface ISolutionItemsManager
    {
        Task UpdateSolution(string templateFilePath, IEnumerable<string> createdFiles);
    }
}
