using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Enum : NamedType, IEnum
    {
        private readonly INamedTypeSymbol symbol;

        public IEnumerable<IEnumValue> Values => EnumValueCollection.Create(symbol);


        private Enum(INamedTypeSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
        }
        

        public static new IEnum Create(INamedTypeSymbol symbol)
        {
            return new Enum(symbol);
        }
    }
}