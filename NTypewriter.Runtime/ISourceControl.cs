using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime
{
    public interface ISourceControl
    {
        void Checkout(string filePath);
    }
}