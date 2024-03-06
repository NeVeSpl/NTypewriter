using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;

namespace NTypewriter.Internals.Functions
{
    internal class LINQFunctions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// Predicate uses C# lambda syntax, the same one that is used by LINQ Where method 
        /// </summary>        
        public static IEnumerable<object> Where(MainTemplateContext context, IEnumerable<object> source, string predicate)
        {
            if (source == null)
                return null;

            var type = source.GetType().GetCollectionElementType();
            var func = context.CompilePredicate(predicate, type);

            return source.Where(func);
        }
    }
}