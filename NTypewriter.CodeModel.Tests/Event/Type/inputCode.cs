using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{

    delegate void MyCusDel();

    class SampleClass
    {
        event Action<int> integer;
        event Func<int?> integer2;
        event Action<int[]> list;
        event Func<List<int>?> list2;
        event MyCusDel str;
    }
}
