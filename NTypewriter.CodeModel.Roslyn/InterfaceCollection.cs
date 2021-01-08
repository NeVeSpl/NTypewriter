using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class InterfaceCollection : List<IInterface>
    {
        private InterfaceCollection(IEnumerable<IInterface> items) : base(items)
        {

        }


        public static IEnumerable<IInterface> Create(IEnumerable<ISymbol> symbols)
        {
            return new InterfaceCollection(symbols
                .OfType<INamedTypeSymbol>()
                .Where(x => x.TypeKind == TypeKind.Interface)
                .Select(x => Interface.Create(x))
                .Where(x => x != null));
        }
    }
}