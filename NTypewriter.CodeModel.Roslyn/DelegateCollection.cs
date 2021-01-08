using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class DelegateCollection : List<IDelegate>
    {
        private DelegateCollection(IEnumerable<IDelegate> items) : base(items)
        {

        }


        public static IEnumerable<IDelegate> Create(IEnumerable<ISymbol> symbols)
        {
            return new DelegateCollection(symbols
                .OfType<INamedTypeSymbol>()
                .Where(x => x.TypeKind == TypeKind.Delegate)
                .Select(x => Delegate.Create(x))
                .Where(x => x != null));
        }
    }
}