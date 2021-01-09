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
        IBase<int> list2;
        Foo str;
        List<int> list;
    }

    interface Foo : IBase<int>
    {

    }

    interface IBase<T> : INested
    {

    }

    interface INested
    {

    }
}
