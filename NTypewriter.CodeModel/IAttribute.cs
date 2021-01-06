using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a declared attribute on a symbol.
    /// </summary>
    public interface IAttribute
    {
        /// <summary>
        /// The full original name of the attribute including namespace and containing class names.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// The name of the attribute without Attribute postfix.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The arguments of the attribute.
        /// </summary>
        IEnumerable<IAttributeArgument> Arguments { get; }
    }
}