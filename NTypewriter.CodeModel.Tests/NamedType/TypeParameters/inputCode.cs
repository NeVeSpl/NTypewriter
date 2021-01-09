using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass<T>
    {
        void Foo1<T>()
        {

        }
        void Foo2<T1, T2>()
        {

        }
        void Foo3()
        {

        }        
    }
}
