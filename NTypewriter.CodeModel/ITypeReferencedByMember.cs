using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ITypeReferencedByMember : IType
    {
        ISymbolBase Parent { get; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
