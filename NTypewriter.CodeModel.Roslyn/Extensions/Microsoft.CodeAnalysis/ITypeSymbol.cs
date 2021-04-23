using System.Linq;

namespace Microsoft.CodeAnalysis
{
    internal static class ITypeSymbolExtensions
    {
        public static object GetDefaultConstantValueAsString(this ITypeSymbol symbol, object defaultConstantValue)
        {
            if (symbol.TypeKind == TypeKind.Enum)
            {
                var enumValues = symbol.GetMembers().OfType<IFieldSymbol>();
                var value = enumValues.FirstOrDefault(x => x.HasConstantValue && x.ConstantValue.Equals(defaultConstantValue));
                if (value != null)
                {
                    return $"{symbol.Name}.{value.Name}";
                }
            }
            return defaultConstantValue?.ToString();
        }
    }
}
