using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class TypeParameterCollection : List<ITypeParameter>, IEnumerable<ITypeParameter>
    {
        private TypeParameterCollection()
        {

        }
        private TypeParameterCollection(IEnumerable<ITypeParameter> items) : base(items)
        {

        }


        public static IEnumerable<ITypeParameter> Create(ISymbol symbol)
        {
            switch(symbol)
            {
                case INamedTypeSymbol namedTypeSymbol:
                    return new TypeParameterCollection(namedTypeSymbol.TypeParameters.Select(x => TypeParameter.Create(x)));
                case IMethodSymbol methodSymbol:
                    return new TypeParameterCollection(methodSymbol.TypeParameters.Select(x => TypeParameter.Create(x)));
            }

            return new TypeParameterCollection();
        }
    }
}