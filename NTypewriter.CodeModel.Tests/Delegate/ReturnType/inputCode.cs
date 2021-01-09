using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{

    delegate int PerformCalculation(int x, int y);

    class SampleClass
    {
        delegate int SampleClassDelegate(int x, int y);
    }


}