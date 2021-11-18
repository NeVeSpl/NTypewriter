using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Runtime
{
    public interface IErrorList
    {
        void Clear();
        void AddError(string source, MessageItem message);
        void Publish();
    }
}
