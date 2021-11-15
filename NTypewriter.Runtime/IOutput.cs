using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime
{
    public interface IOutput
    {
        void Info(string msg);
        void Error(string msg);
    }
}
