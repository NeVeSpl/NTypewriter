using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class FieldCollection : List<IField>
    {
        private FieldCollection(IEnumerable<IField> items) : base(items)
        {
            
        }


        public static IEnumerable<IField> Create(ImmutableArray<ISymbol> symbols)
        {
            return new FieldCollection(symbols
                .OfType<IFieldSymbol>()
                .Where(x => !x.IsCompilerGenerated())
                .Select(x => Field.Create(x))
                .Where(x => x != null));
        }
    }
}