using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a type other than an array, a pointer, a type parameter.
    /// </summary>
    public interface INamedType : IType
    {
        /// <summary>
        /// Determines if the type is declared inside other type
        /// </summary>
        bool IsNested { get; }

        /// <summary>
        /// The type parameters of the type. If the type is not generic, returns an empty collection.
        /// </summary>
        IEnumerable<ITypeParameter> TypeParameters { get; }
    }
}
