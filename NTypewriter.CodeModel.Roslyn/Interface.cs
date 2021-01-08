using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Interface : NamedType, IInterface
    {
        private readonly Lazy<ImmutableArray<ISymbol>> members;
        private readonly INamedTypeSymbol symbol;      

        public IEnumerable<IEvent> Events => EventCollection.Create(members.Value);
        public IEnumerable<IMethod> Methods => MethodCollection.Create(members.Value);
        public IEnumerable<IProperty> Properties => PropertyCollection.Create(members.Value);


        private Interface(INamedTypeSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
            this.members = new Lazy<ImmutableArray<ISymbol>>(() => symbol.GetMembers());
        }


        public static new IInterface Create(INamedTypeSymbol symbol)
        {
            return new Interface(symbol);
        }      
    }
}