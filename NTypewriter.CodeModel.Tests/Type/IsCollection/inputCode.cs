﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    class SampleClass
    {
        int integer;
        int[] array;
        List<int> list;
        IList<int> list;
        System.Collections.IList list;
        string stringField;
        IEnumerable<int> iGenericEnumerable;
        System.Collections.IEnumerable iEnumerable;
        ICollection<int> iGenericCollection;
        System.Collections.ICollection iCollection;
    }
}
