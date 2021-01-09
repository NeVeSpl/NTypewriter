using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{   

    class SampleClass<T>
    {
        int Met1(int par 0, string par1)
        {

        }

        int? Met2()
        {

        }

        void Met3(params int[] par)
        {
            
        }

        void Met4(int par = 0)
        {

        }

        void MetGen<TPar> (TPar par)
        {
            
        }
    }
}