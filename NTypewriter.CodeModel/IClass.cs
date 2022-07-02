using NTypewriter.CodeModel.Traits;
using System.Collections.Generic;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a class.
    /// </summary>
    public interface IClass : INamedType, IHaveFields, IHaveMethods, IHaveProperties
    {
        /// <summary>
        /// The declared base class of this class, or null. Returns null if the class inherits from System.Object.
        /// </summary>
        IClass BaseClass { get; }

        /// <summary>
        /// All instance constructors defined in the class.
        /// </summary>
        IEnumerable<IMethod> Constructors { get; }

        /// <summary>
        /// All events defined in the class.
        /// </summary>
        IEnumerable<IEvent> Events { get; }

        /// <summary>
        /// Determines if the class is sealed
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// All fields defined in the class.
        /// </summary>
        new IEnumerable<IField> Fields { get; }

        /// <summary>
        /// Determines if the class has base class other than System.Object.
        /// </summary>
        bool HasBaseClass { get; }

        /// <summary>
        /// All methods defined in the class.
        /// </summary>
        new IEnumerable<IMethod> Methods { get; }

        /// <summary>
        /// All nested classes defined in the class.
        /// </summary>
        IEnumerable<IClass> NestedClasses { get; }

        /// <summary>
        /// All nested delegates defined in the class.
        /// </summary>
        IEnumerable<IDelegate> NestedDelegates { get; }

        /// <summary>
        /// All nested enums defined in the class.
        /// </summary>
        IEnumerable<IEnum> NestedEnums { get; }

        /// <summary>
        /// All nested interfaces defined in the class.
        /// </summary>
        IEnumerable<IInterface> NestedInterfaces { get; }

        /// <summary>
        /// All properties defined in the class.
        /// </summary>
        new IEnumerable<IProperty> Properties { get; }
    }
}