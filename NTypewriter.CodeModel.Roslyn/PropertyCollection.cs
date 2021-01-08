using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class PropertyCollection : List<IProperty>
    {
        private PropertyCollection(IEnumerable<IProperty> properties) : base(properties)
        {

        }


        internal static PropertyCollection Create(ImmutableArray<ISymbol> symbols)
        {
            return new PropertyCollection(symbols.OfType<IPropertySymbol>().Select(x => Property.Create(x)));
        }
    }
}