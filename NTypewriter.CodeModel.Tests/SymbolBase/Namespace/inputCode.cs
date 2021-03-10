using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class Main
    {
        SampleClass f1;
        SampleClass.SampleNestedClass f2;
        SampleGenericClass<int> f3;
        SampleGenericClass<>.SampleNestedClassInGeneric f4;
        int[] f5;

    }





    class SampleClass
    {
        public class SampleNestedClass
        {

        }
    }
    class SampleGenericClass<T>
    {
        public class SampleNestedClassInGeneric
        {

        }
        class SampleNestedGenericClass<T1>
        {

        }
    }
}
