using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class EventCollection : List<IEvent>
    {
        private EventCollection(IEnumerable<IEvent> items) : base(items)
        {

        }


        public static IEnumerable<IEvent> Create(ImmutableArray<ISymbol> symbols)
        {
            return new EventCollection(symbols.OfType<IEventSymbol>()
                .Select(x => Event.Create(x))
                .Where(x => x != null));
        }
    }
}