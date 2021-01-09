using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass<T>
    {
        int integer;
        int[] array;
        List<int> list;
        List<T> listWithT;
    }
}
