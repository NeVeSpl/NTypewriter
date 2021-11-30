using System;
using System.Collections.Generic;

namespace NTypewriter
{
    public class Configuration
    {
        private readonly List<Type> typesWithCustomFuntions;


        public Configuration(params Type[] typeWithCustomFuntions)
        {
            typesWithCustomFuntions = new List<Type>(typeWithCustomFuntions);
        }


        public Configuration AddCustomFunctions(params Type[] typeWithCustomFuntions)
        {
            typesWithCustomFuntions.AddRange(typeWithCustomFuntions);
            return this;
        }

        internal IEnumerable<Type> GetTypesWithCustomFuntions()
        {
            return typesWithCustomFuntions;
        }
    }
}