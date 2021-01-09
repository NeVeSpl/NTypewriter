using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    [DebuggerDisplay("Test")]
    [SampleAttribute]
    class SampleClass1
    {
        
    }

    class SampleClass2
    {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    class SampleAttribute : System.Attribute
    {

    }
}
