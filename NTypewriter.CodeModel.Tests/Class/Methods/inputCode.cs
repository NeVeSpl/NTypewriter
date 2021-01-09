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
        public void SampleClassFoo()
        {

        }


        private SampleClassConstructor()
        {

        }
    }
    class SampleBaseClass
    {
        public void SampleBaseClassFoo()
        {

        }

        public void GenericMethod<T>()
        {

        }
    }


}



