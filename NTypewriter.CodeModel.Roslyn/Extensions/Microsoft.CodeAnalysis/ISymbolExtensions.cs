namespace Microsoft.CodeAnalysis
{
    internal static class ISymbolExtensions
    {
        public static bool IsCompilerGenerated(this ISymbol symbol)
        {
            return symbol.IsImplicitlyDeclared;
        }
    }
}
