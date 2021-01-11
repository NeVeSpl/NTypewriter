using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor
{
    public interface IEditorConfig : IHaveCustomFunctions
    {
        IEnumerable<string> GetNamespacesToBeSearched();
    }
}
