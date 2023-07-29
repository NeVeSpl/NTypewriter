using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents attribute argument.
    /// </summary>
    public interface IAttributeArgument
    {
        /// <summary>
        /// Determines if arguments is present in the attribute constructor
        /// </summary>
        bool IsFromConstructor { get;  }

        /// <summary>
        /// The name of the argument.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type of the argument.
        /// </summary>
        IType Type { get; }      

        /// <summary>
        /// The value of the argument represented by ITypedConstant or ITypedConstant[]
        /// </summary>
        object Value { get; }
    }
}