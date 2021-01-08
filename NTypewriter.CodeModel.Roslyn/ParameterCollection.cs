using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class ParameterCollection : List<IParameter>
    {
        private ParameterCollection(IEnumerable<IParameter> items) : base(items)
        {

        }


        public static IEnumerable<IParameter> Create(IMethodSymbol symbol)
        {
            return new ParameterCollection(symbol.Parameters.Select(x => Parameter.Create(x)));
        }
    }
}