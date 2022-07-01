using NTypewriter.CodeModel.Traits;
using System.Collections.Generic;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a struct.
    /// </summary>
    public interface IStruct : INamedType, IHaveFields, IHaveMethods, IHaveProperties
    {
        /// <summary>
        /// All instance constructors defined in the struct.
        /// </summary>
        IEnumerable<IMethod> Constructors { get; }

        /// <summary>
        /// All events defined in the struct.
        /// </summary>
        IEnumerable<IEvent> Events { get; }

        /// <summary>
        /// All fields defined in the struct.
        /// </summary>
        new IEnumerable<IField> Fields { get; }

        /// <summary>
        /// All methods defined in the struct.
        /// </summary>
        new IEnumerable<IMethod> Methods { get; }

        /// <summary>
        /// All nested classes defined in the struct.
        /// </summary>
        IEnumerable<IClass> NestedClasses { get; }

        /// <summary>
        /// All nested delegates defined in the struct.
        /// </summary>
        IEnumerable<IDelegate> NestedDelegates { get; }

        /// <summary>
        /// All nested enums defined in the struct.
        /// </summary>
        IEnumerable<IEnum> NestedEnums { get; }

        /// <summary>
        /// All nested interfaces defined in the struct.
        /// </summary>
        IEnumerable<IInterface> NestedInterfaces { get; }

        /// <summary>
        /// All properties defined in the struct.
        /// </summary>
        new IEnumerable<IProperty> Properties { get; }
    }
}
