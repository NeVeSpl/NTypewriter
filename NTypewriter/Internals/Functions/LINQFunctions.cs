using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.Internals.Functions
{
    internal class LINQFunctions
    {
        public static IEnumerable<object> Where(MainTemplateContext context, IEnumerable<object> source, string predicate)
        {
            var func = context.CompileExpression(predicate);
            return source.Where(func);
        }
    }
}