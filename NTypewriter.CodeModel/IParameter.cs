using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a method parameter.
    /// </summary>
    public interface IParameter : ISymbolBase
    {
        /// <summary>
        /// The default value of the parameter
        /// </summary>
        object DefaultValue { get; }

        /// <summary>
        /// Determines if the parameter has a default value to be passed when no value is provided as an argument to a call.
        /// </summary>
        bool HasDefaultValue { get; }

        /// <summary>
        /// The type of the parameter
        /// </summary>
        IType Type { get; }
    }
}