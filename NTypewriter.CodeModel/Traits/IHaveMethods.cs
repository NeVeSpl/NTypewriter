using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel.Traits
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IHaveMethods
    {
        IEnumerable<IMethod> Methods { get; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
