using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IHaveCustomFunctions
    {
        IEnumerable<Type> TypesThatContainCustomFunctions { get; set; }    
    }
}
