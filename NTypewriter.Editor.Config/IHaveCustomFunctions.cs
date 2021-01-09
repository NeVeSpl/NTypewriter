using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor
{
    public interface IHaveCustomFunctions
    {
        IEnumerable<Type> GetTypesThatContainsStaticFunctionsToImport();        
    }
}
