using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{

    enum OutsideEnum
    {
        One = 1,
        Two = 2
    }

    class SampleClass
    {
        enum InsideEnum
        {
            Zero = 0x01,
            One = 0x02
        }
    }


}