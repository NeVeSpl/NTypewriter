using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class EnumCollection : List<IEnum>
    {
        private EnumCollection(IEnumerable<IEnum> items) : base(items)
        {

        }


        public static IEnumerable<IEnum> Create(IEnumerable<ISymbol> symbols)
        {
            return new EnumCollection(symbols
                .OfType<INamedTypeSymbol>()
                .Where(x => x.TypeKind == TypeKind.Enum)
                .Select(x => Enum.Create(x))
                .Where(x => x!= null));
        }
    }
}