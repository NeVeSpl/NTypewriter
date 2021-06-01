using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal class Type : SymbolBase, IType, ITypeReferencedByMember
    {
        private readonly ITypeSymbol symbol;
        private ISymbolBase parent;

        public IType BaseType => NTypewriter.CodeModel.Roslyn.Type.CreateBaseType(symbol.BaseType);
        public bool IsAnonymousType => symbol.IsAnonymousType;
        public bool IsEnumerable => symbol.AllInterfaces.Any(x => x.ToString() == "System.Collections.IEnumerable") || this.FullName == "System.Collections.IEnumerable";
        public bool IsDelegate => symbol.TypeKind == TypeKind.Delegate;
        public bool IsEnum => symbol.TypeKind == TypeKind.Enum;
        public bool IsCollection => symbol.AllInterfaces.Any(x => x.ToString() == "System.Collections.ICollection" || this.FullName.StartsWith("System.Collections.Generic.ICollection") || this.FullName == "System.Collections.ICollection");
        public bool IsGeneric => (symbol is INamedTypeSymbol x) && x.IsGenericType;
        public bool IsInterface => symbol.TypeKind == TypeKind.Interface;
        public bool IsNullable => symbol.NullableAnnotation == NullableAnnotation.Annotated;      
        public bool IsPrimitive
        {
            get
            {
                switch(symbol.SpecialType)
                {
                    case SpecialType.System_Boolean:
                    case SpecialType.System_SByte:
                    case SpecialType.System_Int16:
                    case SpecialType.System_Int32:
                    case SpecialType.System_Int64:
                    case SpecialType.System_Byte:
                    case SpecialType.System_UInt16:
                    case SpecialType.System_UInt32:
                    case SpecialType.System_UInt64:
                    case SpecialType.System_Single:
                    case SpecialType.System_Double:
                    case SpecialType.System_Char:
                    case SpecialType.System_String:
                    case SpecialType.System_Object:
                        return true;
                }
                return false;
            }
        }
        public bool IsReferenceType => symbol.IsReferenceType;
        public bool IsValueType => symbol.IsValueType;
        public IEnumerable<IInterface> Interfaces => InterfaceCollection.Create(symbol.Interfaces);
        public IEnumerable<IInterface> AllInterfaces => InterfaceCollection.Create(symbol.AllInterfaces);
        public IType ArrayType => symbol is IArrayTypeSymbol arraySymbol ? NTypewriter.CodeModel.Roslyn.Type.Create(arraySymbol.ElementType) : null;
        public IEnumerable<IType> TypeArguments => TypeCollection.CreateTypeArguments(symbol);
        public override string Name
        {
            get
            {
                return symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);                
            }
        }

        ISymbolBase ITypeReferencedByMember.Parent => parent;

        private protected Type(ITypeSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
        }    


        public static Type CreateBaseType(ITypeSymbol symbol)
        {
            if ((symbol?.SpecialType != SpecialType.System_Object) && (symbol?.SpecialType != SpecialType.System_ValueType))
            {
                return Create(symbol);
            }
            return null;
        }
        public static Type Create(ITypeSymbol symbol, ISymbolBase parent = null)
        {
            if (symbol != null)
            {
                Type createdType = null;

                switch (symbol)
                {
                    case INamedTypeSymbol namedTypeSymbol when namedTypeSymbol.TypeKind == TypeKind.Class && namedTypeSymbol.SpecialType == SpecialType.None:
                        createdType = Class.Create(namedTypeSymbol);
                        break;
                    default:
                        createdType = new Type(symbol);
                        break;
                }


                createdType.parent = parent;
                return createdType;
            }
            return null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}