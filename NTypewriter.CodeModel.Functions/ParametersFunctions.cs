using System.Linq;
using System.Collections.Generic;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Parameters functions
    /// </summary>
    public static class ParametersFunctions
    {
        /// <summary>
        ///
        /// </summary>
        public static IEnumerable<string> ToTypeScript(this IEnumerable<IParameter> parameters, string nullableType = "null")
        {
            return parameters.Select(x => $"{x.BareName}: {x.Type.ToTypeScriptType(nullableType)}");
        }
    }
}
