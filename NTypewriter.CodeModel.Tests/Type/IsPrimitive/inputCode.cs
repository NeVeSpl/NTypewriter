using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass
    {
        int integer;       
        int? integer2;
        List<int> list;
        List<int>? list2;
        string str;
        Enumik enumik;
        int[] array;
    }

    enum Enumik { First }
}
