using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class MethodCollection : List<IMethod>
    {
        private MethodCollection(IEnumerable<IMethod> items) : base(items)
        {

        }


        public static MethodCollection CreateConstructors(ImmutableArray<ISymbol> members)
        {
            return Create(members, MethodKind.Constructor);
        }

        public static MethodCollection Create(ImmutableArray<ISymbol> members, MethodKind methodKind = MethodKind.Ordinary)
        {
            return new MethodCollection(members
                .OfType<IMethodSymbol>()
                .Where(x => !x.IsCompilerGenerated())
                .Where(x => x.MethodKind == methodKind)
                .Select(x => Method.Create(x)));
        }
    }
}