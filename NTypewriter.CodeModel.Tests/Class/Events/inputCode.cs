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
        public event Action SampleClassEvent;


        private SampleClassConstructor()
        {

        }
    }


    abstract class SampleBaseClass
    {
        private event Action SampleBaseClassEventPrivate;
        protected event Action SampleBaseClassEventProtected;

        public SampleBaseClassConstructor()
        {

        }
    }


}
