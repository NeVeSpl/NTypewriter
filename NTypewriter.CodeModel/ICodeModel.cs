using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Representation of the code.
    /// ICodeModel is the entry point of accessing information about the code.
    /// </summary>
    public interface ICodeModel
    {
        /// <summary>
        /// All classes defined in the code
        /// </summary>
        IEnumerable<IClass> Classes { get;  }

        /// <summary>
        /// All delegates defined in the code
        /// </summary>
        IEnumerable<IDelegate> Delegates { get; }

        /// <summary>
        /// All enums defined in the code
        /// </summary>
        IEnumerable<IEnum> Enums { get; }

        /// <summary>
        /// All interfaces defined in the code
        /// </summary>
        IEnumerable<IInterface> Interfaces { get; }

        /// <summary>
        /// All structs defined in the code
        /// </summary>
        IEnumerable<IStruct> Structs { get; }
    }
}