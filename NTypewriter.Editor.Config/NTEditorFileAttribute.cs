using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NTEditorFileAttribute : Attribute
    {
    }
}
