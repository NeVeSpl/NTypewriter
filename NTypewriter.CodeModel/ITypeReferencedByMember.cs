using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    public interface ITypeReferencedByMember : IType
    {
        ISymbolBase Parent { get; }
    }
}
