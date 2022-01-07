using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Set of functions that operates on ISymbolBase
    /// </summary>
    public static partial class SymbolFunctions
    {
        /// <summary>
        /// Checks if symbol is decorated with an attribute
        /// </summary>
        public static bool HasAttribute(this ISymbolBase symbol, string attributeName)
        {
            return symbol.Attributes.Any(y => y.Name == attributeName);
        }
    }
}
