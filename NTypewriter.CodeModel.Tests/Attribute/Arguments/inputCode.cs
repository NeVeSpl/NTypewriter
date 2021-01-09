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

    [SampleAttribute(arrayOfInts: new[] { 1, 2, 3 })]
    class SampleClass2
    {

    }

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal class SampleAttribute : System.Attribute
    {

        public SampleAttribute()
        {

        }


        public SampleAttribute(int[] arrayOfInts)
        {

        }
        public SampleAttribute(string par1, int par2)
        {

        }
    }
}
