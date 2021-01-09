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
        /// <summary>
        /// very short summary
        /// </summary>
        /// <param name="a">p1</param>
        /// <param name="b">p2</param>
        /// <returns>RR</returns>
        int Foo(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// very short summary 3
        /// </summary>       
         int FooB(int a, int b)
        {
            return a + b;
        }


        int FooE(int a, int b)
        {
            return a + b;
        }
    }


}