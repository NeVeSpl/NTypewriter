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
        const string s1 = "";
        const string s2;
        readonly static int? integer2 = 1;
        int[] list = default;
        const CancellationToken cancel = default;
    }
}
