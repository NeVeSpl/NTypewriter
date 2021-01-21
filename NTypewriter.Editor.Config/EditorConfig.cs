using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public class EditorConfig : IEditorConfig
    {
        public virtual IEnumerable<string> GetNamespacesToBeSearched()
        {
            return Enumerable.Empty<string>();
        }

        public virtual IEnumerable<Type> GetTypesWithCustomFunctions()
        {
            return Enumerable.Empty<Type>();
        }
    }
}