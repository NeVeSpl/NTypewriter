using System;
using System.Collections.Generic;
using System.Text;
using NTypewriter.Ports;

namespace NTypewriter.Runtime.Scripting
{
    internal class ExpressionCompiler : IExpressionCompiler
    {
        public Func<object, bool> CompilePredicate(string predicate)
        {
            return x => true;
        }
    }
}
