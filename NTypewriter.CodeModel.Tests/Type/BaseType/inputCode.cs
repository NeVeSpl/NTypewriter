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
        BaseClass<int> list2;
        Foo str;
    }

    class Foo : BaseClass<int>
    {

    }

    class BaseClass<T>
    {

    }
}
