using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Set of functions that operates on IType
    /// </summary>
    public static partial class TypeFunctions
    {
        /// <summary>
        /// Returns the first TypeArgument of a generic type or the type itself if it's not generic.
        /// </summary>
        public static IType Unwrap(this IType type)
        {
            return type.TypeArguments?.FirstOrDefault() ?? type;
        }
    }
}
