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
        void Foo1(int par) { };
        void Foo2<T2>() { };

        class SampleNestedClass
        {
            void Foo3() { };
        }
    }
    class SampleGenericClass<T>
    {
        void Foo4() { };
        void Foo5<T5>() { };

        class SampleNestedClassInGeneric
        {
            void Foo6() { };
        }
        class SampleNestedGenericClass<TT>
        {
            void Foo7() { };
        }
    }
}