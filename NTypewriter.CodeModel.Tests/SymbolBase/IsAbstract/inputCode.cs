using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    abstract class SampleClass
    {       
        int  Foo1() { };
        abstract int[]  Foo2();
        virtual protected List<int> Foo3() { };
    }
}
