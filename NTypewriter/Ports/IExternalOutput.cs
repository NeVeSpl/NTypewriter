using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Ports
{
    public interface IExternalOutput
    {
        void Write(string text);
    }
}
