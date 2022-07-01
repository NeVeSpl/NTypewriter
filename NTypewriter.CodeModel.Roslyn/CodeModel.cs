using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel.Roslyn.Internals;

namespace NTypewriter.CodeModel.Roslyn
{
    public sealed class CodeModel : ICodeModel
    {
        private readonly Compilation compilation;
        private readonly CodeModelConfiguration configuration;
        private readonly Lazy<IEnumerable<IClass>> classes;
        private readonly Lazy<IEnumerable<IDelegate>> delegates;    
        private readonly Lazy<IEnumerable<IEnum>> enums;     
        private readonly Lazy<IEnumerable<IInterface>> interfaces;
        private readonly Lazy<IEnumerable<IStruct>> structs;


        public IEnumerable<IClass> Classes => classes.Value;
        public IEnumerable<IDelegate> Delegates => delegates.Value;
        public IEnumerable<IEnum> Enums => enums.Value;
        public IEnumerable<IInterface> Interfaces => interfaces.Value;
        public IEnumerable<IStruct> Structs => structs.Value;


        public CodeModel(Compilation compilation, CodeModelConfiguration configuration)
        {
            this.compilation = compilation;
            this.configuration = configuration;
            this.classes = new Lazy<IEnumerable<IClass>>(() => ClassCollection.Create(GetTypes(TypeKind.Class)));
            this.delegates = new Lazy<IEnumerable<IDelegate>>(() => DelegateCollection.Create(GetTypes(TypeKind.Delegate)));
            this.enums = new Lazy<IEnumerable<IEnum>>(() => EnumCollection.Create(GetTypes(TypeKind.Enum)));
            this.interfaces = new Lazy<IEnumerable<IInterface>>(() => InterfaceCollection.Create(GetTypes(TypeKind.Interface)));
            this.structs = new Lazy<IEnumerable<IStruct>>(() => StructCollection.Create(GetTypes(TypeKind.Struct)));
        }


        private List<INamedTypeSymbol> GetTypes(TypeKind typeKind)
        {
            var symbolFinder = new SymbolFinder(typeKind, 
                configuration?.NamespacesToFilterBy,
                configuration?.OmitSymbolsFromReferencedAssemblies == true,
                compilation.AssemblyName);
            compilation.GlobalNamespace.Accept(symbolFinder);
            return symbolFinder.Result;
        }
    }
}