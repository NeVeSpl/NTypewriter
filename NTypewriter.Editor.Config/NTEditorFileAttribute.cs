using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    [Obsolete("This attribute is obsolete, and will be removed in the future. Please change the file extension to *.nt.cs instead of using this attribute.")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NTEditorFileAttribute : Attribute
    {
    }
}
