using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Runtime
{
    public interface IStatus
    {
        Task Update(string text, int progress = 0, int total = 0);
    }
}
