using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IHaveCustomFunctions
    {
        [Obsolete("This property is obsolete, and will be removed in the future. Classes that contain custom funtions are now detected automaticly, there is no need anymore to list them here, you can remove this property.")]
        IEnumerable<Type> TypesThatContainCustomFunctions { get; set; }    
    }
}
