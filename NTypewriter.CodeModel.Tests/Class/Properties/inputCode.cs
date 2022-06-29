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
        public string ClassProp { get; set; }
    }
    
    record SampleRecord(int RecordProp)

    record class SampleRecordClass(int RecordClassProp)
}