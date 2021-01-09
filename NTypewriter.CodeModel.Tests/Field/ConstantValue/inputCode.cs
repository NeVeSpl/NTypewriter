using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Task;
using System.Threading.Tasks;
using System.Threading;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass
    {
        const int integer = 7;
        const string s1 = "r";
        const string s2 = null;
        const int[] list = default;
        const bool b = true;
        const Enumee e = Enumee.One;
        readonly static int? integer2 = 1;        

    }

    enum Enumee { One, Two }
}
