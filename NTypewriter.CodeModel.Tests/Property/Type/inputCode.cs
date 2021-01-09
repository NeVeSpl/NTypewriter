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
        int Integer { get; set; }
        int? IntegerN { get; set; }

        public T this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
    }
}
