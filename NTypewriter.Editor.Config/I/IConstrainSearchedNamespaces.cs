using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IConstrainSearchedNamespaces
    {
        IEnumerable<string> GetNamespacesToBeSearched();
    }
}
