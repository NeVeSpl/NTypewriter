using System;
using System.Collections.Generic;

namespace NTypewriter
{
    public class Configuration
    {
        internal readonly List<Type> typesWithCustomFuntions = new List<Type>();


        public Configuration()
        {
            
        }


        public Configuration AddCustomFunctions(params Type[] typeWithCustomFuntions)
        {
            typesWithCustomFuntions.AddRange(typeWithCustomFuntions);
            return this;
        }
    }
}