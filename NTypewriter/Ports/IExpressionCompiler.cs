using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Ports
{
    public interface IExpressionCompiler
    {
        Func<object, bool> CompilePredicate(string predicate);
    }
}
