using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NTypewriter.Tests.CodeModel
{
    enum EnumWithDocComments
    {
        One = 1,
        Two = 2,
        Three = 3,
        /// <summary>
        /// This is the number four. (4)
        /// </summary>
        Four = 4,
        /// <summary>
        /// This is the number five. (5) 
        /// </summary>
        Five = 5,
    }
}