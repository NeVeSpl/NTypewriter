using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a type.
    /// </summary>
    public interface IType : ISymbolBase
    {
        /// <summary>
        /// The declared base type of this type, or null. Returns null if the class inherits from System.Object or if the type is a value type.
        /// </summary>
        IType BaseType { get; }

        /// <summary>
        /// Determines if the type is anonymous
        /// </summary>
        bool IsAnonymousType { get; }

        /// <summary>
        /// Determines if the type is a collection
        /// </summary>
        bool IsCollection { get; }

        /// <summary>
        /// Determines if the type is a dynamic
        /// </summary>
        bool IsDynamic { get; }

        /// <summary>
        /// Determines if the type is a delegate
        /// </summary>
        bool IsDelegate { get; }

        /// <summary>
        /// Determines if the type is an interface
        /// </summary>
        bool IsInterface { get; }

        /// <summary>
        /// Determines if the type is an enum
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// Determines if the type is enumerable
        /// </summary>
        bool IsEnumerable { get; }

        /// <summary>
        /// Determines if the type is generic
        /// </summary>
        bool IsGeneric { get; }

        /// <summary>
        /// Determines if the type is nullable
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// Determines if the type is primitive
        /// </summary>
        bool IsPrimitive { get; }

        /// <summary>
        /// Determines if the type is a record
        /// </summary>
        bool IsRecord { get; }

        /// <summary>
        /// Determines if the type is a tuple
        /// </summary>
        bool IsTuple { get; }

        /// <summary>
        /// Determines if the type is a reference type
        /// </summary>
        bool IsReferenceType { get; }

        /// <summary>
        /// Determines if the type is a value type
        /// </summary>
        bool IsValueType { get; }

        /// <summary>
        /// The type of the elements stored in the array.
        /// </summary>
        IType ArrayType { get; }

        /// <summary>
        /// The set of interfaces that this type directly implements. This set does not include interfaces that are base interfaces of directly implemented interfaces.
        /// </summary>
        IEnumerable<IInterface> Interfaces { get; }

        /// <summary>
        /// The list of all interfaces of which this type is a declared subtype, excluding this type itself.
        /// </summary>
        IEnumerable<IInterface> AllInterfaces { get; }

        /// <summary>
        /// The type arguments that have been substituted for the type parameters
        /// </summary>
        IEnumerable<IType> TypeArguments { get; }
    }
}