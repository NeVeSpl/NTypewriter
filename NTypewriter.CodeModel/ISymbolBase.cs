using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a symbol (namespace, class, method, parameter, etc.)
    /// </summary>
    public interface ISymbolBase
    {
        /// <summary>
        /// All attributes declared on the symbol.
        /// </summary>
        IEnumerable<IAttribute> Attributes { get; }

        /// <summary>
        /// The XML documentation for the comment associated with the symbol.
        /// </summary>
        IDocumentationCommentXml DocComment { get; }

        /// <summary>
        /// Determines if the symbol is abstract
        /// </summary>
        bool IsAbstract { get; }

        /// <summary>
        /// Determines if the symbol is an array
        /// </summary>
        bool IsArray { get; }

        /// <summary>
        /// Determines if the symbol is an event
        /// </summary>
        bool IsEvent { get; }

        /// <summary>
        /// Determines if the symbol is a field
        /// </summary>
        bool IsField { get; }

        /// <summary>
        /// Determines if the symbol is a method
        /// </summary>
        bool IsMethod { get; }

        /// <summary>
        /// Determines if the symbol is a property
        /// </summary>
        bool IsProperty { get; }

        /// <summary>
        /// Determines if the symbol is public
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Determines if the symbol is static
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// Determines if the symbol is a type parameter
        /// </summary>
        bool IsTypeParameter { get; }

        /// <summary>
        /// The type that contains this symbol.
        /// </summary>
        INamedType ContainingType { get; }

        /// <summary>
        /// The Namespace + the name of the symbol
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// The name of the symbol
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The prefix of the Name that consists only letters and digits.
        /// </summary>
        string BareName { get; }

        /// <summary>
        /// The nearest enclosing namespace for the symbol.
        /// </summary>
        string Namespace { get; }
    }
}