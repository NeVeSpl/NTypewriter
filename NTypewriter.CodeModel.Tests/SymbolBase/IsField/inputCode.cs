using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass : Base
    {
        int field;
        event Action SampleEvent;
        int Property { get; set; }
        int Foo() { return 7; }
    }    
}
