using System;
using System.Collections.Generic;
using System.Linq;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class AttributeCollection : List<IAttribute>
    {
        private AttributeCollection(IEnumerable<IAttribute> collection) : base(collection)
        {
        }        


        public static AttributeCollection Create(Microsoft.CodeAnalysis.ISymbol symbol)
        {
            return new AttributeCollection(symbol.GetAttributes().Select(x => new Attribute(x)));
        }


        public override string ToString()
        {
            if (Count == 0)
            {
                return String.Empty;
            }
            return base.ToString();
        }

        public static implicit operator string(AttributeCollection instance)
        {
            return instance.ToString();
        }
    }
}