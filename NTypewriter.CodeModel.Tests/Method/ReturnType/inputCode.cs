using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{

    delegate void MyCusDel();

    class SampleClass<T>
    {
        int Met1()
        {

        }

        static int? Met2()
        {

        }

        async Task Met3()
        {
            return Task.CompletedTask;
        }
        async Task<int> Met4()
        {
            return Task.FromResult<int>(7);
        }

        TRet MetGen<TRet> ()
        {
            return default;
        }
    }
}