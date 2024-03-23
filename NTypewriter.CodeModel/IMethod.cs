using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a method.
    /// </summary>
    public interface IMethod : ISymbolBase
    {
        /// <summary>
        ///  Determines if the method is an async method
        /// </summary>
        bool IsAsync { get; }

        /// <summary>        
        /// Returns true for 'init' set accessors, and false otherwise.
        /// </summary>
        bool IsInitOnly { get; }

        /// <summary>
        /// Determines if the method is generic (it has any type parameters)
        /// </summary>
        bool IsGeneric { get; }

        /// <summary>
        /// Determines if the method is override
        /// </summary>
        bool IsOverride { get; }

        /// <summary>
        /// Determines if the method is sealed
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// All parameters of the method.
        /// </summary>
        IEnumerable<IParameter> Parameters { get; }      

        /// <summary>
        /// The return type of the method.
        /// </summary>
        IType ReturnType { get; }

        /// <summary>
        /// The type parameters of the method. If the method is not generic, returns an empty collection.
        /// </summary>
        IEnumerable<ITypeParameter> TypeParameters { get; }
    }
}