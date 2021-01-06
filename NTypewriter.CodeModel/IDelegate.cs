using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a delegate.
    /// </summary>
    public interface IDelegate : INamedType
    {
        /// <summary>
        /// The parameters of this delegate. If this delegate has no parameters, returns an empty collection.
        /// </summary>
        IEnumerable<IParameter> Parameters { get; }

        /// <summary>
        /// The return type of the delegate.
        /// </summary>
        IType ReturnType { get; }      
    }
}