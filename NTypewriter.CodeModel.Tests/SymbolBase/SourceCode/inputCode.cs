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
        public SampleClass()
        {
            
        }


        public void Foo(string par)
        {
            // void
        }
    }
    class SampleGenericClass<T>
    {
        public int Foo2(string par)
        {
            return 1; 

        }
    }
}
