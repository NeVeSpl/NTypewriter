using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
    internal static class ISymbolExtensions
    {
        public static bool IsCompilerGenerated(this ISymbol symbol)
        {
            var attributes = symbol.GetAttributes();

            if (attributes.Any(x => x.AttributeClass.Name == nameof(CompilerGeneratedAttribute)))
            {
                return true;
            }           
            
            return symbol.IsImplicitlyDeclared;
        }
    }
}
