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
        public int publicFoo;      
        private int privateFoo;
        protected int protectedFoo;
        internal int internalFoo;
        protected internal int protectedInternalFoo;
        private protected int privateProtectedFoo;
    }
}
