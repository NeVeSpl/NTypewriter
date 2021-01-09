using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Symbols functions
    /// </summary>
    public static class SymbolsFunctions
    {
        /// <summary>
        /// Filters symbols by the beginning of their namespace
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNamespaceStartsWith(this IEnumerable<ISymbolBase> symbols, string prefix)
        {
            var result = symbols.Where(x => x.Namespace.StartsWith(prefix));
            return result;
        }


        /// <summary>
        /// Filters symbols by the end of their namespace
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNamespaceEndsWith(this IEnumerable<ISymbolBase> symbols, string prefix)
        {
            var result = symbols.Where(x => x.Namespace.EndsWith(prefix));
            return result;
        }


        /// <summary>
        /// Filters symbols by regex pattern
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNamespaceMatches(this IEnumerable<ISymbolBase> symbols, string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled);
            var result = symbols.Where(x => regex.IsMatch(x.Namespace));
            return result;
        }


        /// <summary>
        /// Filters symbols by the beginning of their name
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNameStartsWith(this IEnumerable<ISymbolBase> symbols, string prefix)
        {
            var result = symbols.Where(x => x.Name.StartsWith(prefix));
            return result;
        }


        /// <summary>
        /// Filters symbols by the end of their name
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNameEndsWith(this IEnumerable<ISymbolBase> symbols, string prefix)
        {
            var result = symbols.Where(x => x.Name.EndsWith(prefix));
            return result;
        }


        /// <summary>
        /// Filters symbols by a regex pattern
        /// </summary>
        public static IEnumerable<ISymbolBase> WhereNameMatches(this IEnumerable<ISymbolBase> symbols, string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled);
            var result = symbols.Where(x => regex.IsMatch(x.Name));
            return result;
        }

        /// <summary>
        /// Filters symbols by the presence of an attribute
        /// </summary>
        public static IEnumerable<ISymbolBase> ThatHaveAttribute(this IEnumerable<ISymbolBase> symbols, string attributeName)
        {
            var result = symbols.Where(x => x.Attributes.Any(y => y.Name == attributeName));
            return result;
        }


        /// <summary>
        /// Filters symbols by the public access modifier
        /// </summary>
        public static IEnumerable<ISymbolBase> ThatArePublic(this IEnumerable<ISymbolBase> symbols)
        {
            var result = symbols.Where(x => x.IsPublic);
            return result;
        }
    }
}