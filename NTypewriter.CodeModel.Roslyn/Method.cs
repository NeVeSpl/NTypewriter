using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Method : SymbolBase, IMethod
    {
        private readonly IMethodSymbol symbol;       

        public bool IsGeneric => symbol.IsGenericMethod;
        public IEnumerable<IType> TypeArguments => TypeCollection.CreateTypeArguments(symbol);
        public IEnumerable<ITypeParameter> TypeParameters => TypeParameterCollection.Create(symbol);
        public IEnumerable<IParameter> Parameters => ParameterCollection.Create(symbol);
        public IType ReturnType => Type.Create(symbol.ReturnType);
        public bool IsAsync => symbol.IsAsync;
        public bool IsOverride => symbol.IsOverride;
        public bool IsSealed => symbol.IsSealed;

        public override string Name
        {
            get
            {
                return symbol.ToDisplayString(symbolDisplayFormat);
            }
        }

        public override string FullName
        {
            get
            {
                return symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            }
        }


        private Method(IMethodSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;         
        }


        public static Method Create(IMethodSymbol symbol)
        {
            return new Method(symbol);
        }
    }
}