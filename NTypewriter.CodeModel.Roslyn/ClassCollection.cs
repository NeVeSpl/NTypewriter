using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class ClassCollection : List<IClass>
    {
        private ClassCollection(IEnumerable<IClass> collection) : base(collection)
        {

        }
         

        public static ClassCollection Create(IEnumerable<ISymbol> symbols)
        {
            return new ClassCollection(symbols
                .OfType<INamedTypeSymbol>()
                .Where(x => !x.IsCompilerGenerated())
                .Where(x => x.TypeKind == TypeKind.Class)
                .Select(x => Class.Create(x))
                .Where(x => x != null));
        }
    }
}