using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class TypeCollection : List<IType>
    {
        private TypeCollection()
        {

        }
        private TypeCollection(IEnumerable<IType> items) : base(items)
        {

        }



        public static IEnumerable<IType> CreateTypeArguments(ISymbol symbol)
        {
            switch(symbol)
            {
                case INamedTypeSymbol namedTypeSymbol:
                    return new TypeCollection(namedTypeSymbol.TypeArguments.Select(x => Type.Create(x)));
                case IMethodSymbol methodSymbol:
                    return new TypeCollection(methodSymbol.TypeArguments.Select(x => Type.Create(x)));
            }

            return new TypeCollection();
        }
    }
}