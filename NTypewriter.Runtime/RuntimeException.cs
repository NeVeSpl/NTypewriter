using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime
{
    public class RuntimeException : Exception
    {
        public RuntimeException(string message) : base(message)
        {
        }
    }
}
