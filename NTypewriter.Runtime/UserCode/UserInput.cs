using System;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace NTypewriter.Runtime.UserCode
{
    public class UserInput
    {
        public IEditorConfig GlobalConfig { get; set; } = new EditorConfig();
        public IEnumerable<Type> TypesThatMayContainCustomFunctions { get; set; } = new List<Type>();
    }
}