using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass
    {
        [DebuggerDisplay("foo")]
        int integer;
        [DebuggerDisplay("foo", Type : "fooType")]
        int[] array;

        List<int> list;
    }
}
