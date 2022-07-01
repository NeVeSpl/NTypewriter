using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    [DebuggerDisplay("Struct, Name = {Name}")]
    internal sealed class Struct : NamedType, IStruct
    {
        private readonly INamedTypeSymbol symbol;
        private readonly Lazy<ImmutableArray<ISymbol>> members;
       

        public IEnumerable<IMethod> Constructors => MethodCollection.CreateConstructors(members.Value);
        public IEnumerable<IEvent> Events => EventCollection.Create(members.Value);
        public IEnumerable<IField> Fields => FieldCollection.Create(members.Value);        
        public IEnumerable<IMethod> Methods => MethodCollection.Create(members.Value);
        public IEnumerable<IClass> NestedClasses => ClassCollection.Create(members.Value);
        public IEnumerable<IDelegate> NestedDelegates => DelegateCollection.Create(members.Value);
        public IEnumerable<IEnum> NestedEnums => EnumCollection.Create(members.Value);
        public IEnumerable<IInterface> NestedInterfaces => InterfaceCollection.Create(members.Value);
        public IEnumerable<IProperty> Properties => PropertyCollection.Create(members.Value);


        private Struct(INamedTypeSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
            this.members = new Lazy<ImmutableArray<ISymbol>>(() => symbol.GetMembers());
        }


        public static new Struct Create(INamedTypeSymbol symbol)
        {
            if (symbol?.SpecialType == SpecialType.None)
            {
                return new Struct(symbol);
            }
            return null;
        }
    }
}
