using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass
    {
        
        (int i, int j) Foo()
        {
            return (1, 2);
        }
    }
}
