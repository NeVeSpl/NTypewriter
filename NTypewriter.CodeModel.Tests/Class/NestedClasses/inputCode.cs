 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{

    class SampleClass : SampleBaseClass<int>
    {
        

        class NestedClassInSampleClass
        {

        }
    }
    class SampleBaseClass<T>
    {
        class NestedClassInSampleBaseClass<T>
        {

        }
    }


}



