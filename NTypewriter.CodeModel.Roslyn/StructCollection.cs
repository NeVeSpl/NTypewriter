using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal class StructCollection : List<IStruct>
    {
        private StructCollection(IEnumerable<IStruct> collection) : base(collection)
        {

        }


        public static StructCollection Create(IEnumerable<ISymbol> symbols)
        {
            return new StructCollection(symbols
                .OfType<INamedTypeSymbol>()
                .Where(x => !x.IsCompilerGenerated())
                .Where(x => x.TypeKind == TypeKind.Struct)
                .Select(x => Struct.Create(x))
                .Where(x => x != null));
        }
    }
}
