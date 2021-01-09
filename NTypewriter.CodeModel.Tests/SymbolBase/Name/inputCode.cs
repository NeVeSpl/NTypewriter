using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    
    class SampleGenericClass<T>
    {
        void Foo() { };
        void Foo1<T>() { };
     

        class Nested
        {
            void Foo2() { };
        }
    }
}
