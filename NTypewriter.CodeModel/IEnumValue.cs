using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a named constant which is a member of an enum.
    /// </summary>
    public interface IEnumValue
    {
        /// <summary>
        /// The name of the enum member
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The value of the enum member
        /// </summary>
        object Value { get; }

        /// <summary>
        /// All attributes declared on the enum value.
        /// </summary>
        IEnumerable<IAttribute> Attributes { get; }

        /// <summary>
        /// The XML documentation for the comment associated with the symbol.
        /// </summary>
        IDocumentationCommentXml DocComment { get; }
    }
}