using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{



    class SampleClass
    {
        public int PropGetSet { get; set; }
        public int PropGet { get; }
        public int PropGetInternalSet { get; internal set; }
        public int PropGetPrivateSet { get; private set; }
        public int PropGetProtectedSet { get; protected set; }
        public int PropGetInit { get; init; }
        public required string PropRequiredGetSet { get; set; }
        public required string PropRequiredGetInit { get; init; }
        protected int ProtectedPropGetSet { get; set; }
        internal int InternalPropGetSet { get; set; }
    }
}
