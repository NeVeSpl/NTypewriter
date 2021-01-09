using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
   
    class SampleClass : SampleBaseClass
    {
        public int SampleClassField;


        private SampleClassConstructor()
        {

        }
    }


    class SampleBaseClass
    {
        private readonly static int SampleBaseClassPrivateStaticReadonlyField;
        public string SampleBaseClassField;

        public SampleBaseClassConstructor()
        {

        }
    }


}
