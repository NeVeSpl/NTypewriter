using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
        public static IEnumerable<string> ToTypeScript(this IEnumerable<IParameter> parameters)
        {
            return parameters.Select(x => $"{x.BareName}: {x.Type.ToTypeScriptType()}");
        }
    }
}
