using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a field.
    /// </summary>
    public interface IField : ISymbolBase
    {
        /// <summary>
        /// Constant value assigned to this field
        /// </summary>
        object ConstantValue { get; }

        /// <summary>
        ///  Returns false if the field wasn't declared as "const", or constant value was omitted or erroneous. True otherwise.
        /// </summary>
        bool HasConstantValue { get; }

        /// <summary>
        /// Determines if the field was declared as "const".
        /// </summary>
        bool IsConst { get; }

        /// <summary>
        /// Determines if the field was declared as "readonly".
        /// </summary>
        bool IsReadOnly { get; }       

        /// <summary>
        /// The type of the field.
        /// </summary>
        IType Type { get; }
    }
}