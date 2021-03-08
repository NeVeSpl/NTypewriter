using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NTypewriter.Tests.CodeModel
{
    enum OutsideEnum
    {
        [Description("First one")]
        One = 1,
        [Description("Second one")]
        Two = 2,
        Three = 3,
    }
}