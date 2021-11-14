using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime.Configuration
{
    public interface IOutput
    {
        void Info(string msg);
        void Error(string msg);
    }
}
