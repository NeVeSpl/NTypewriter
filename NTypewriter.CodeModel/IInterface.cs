using NTypewriter.CodeModel.Traits;
using System.Collections.Generic;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a interface.
    /// </summary>
    public interface IInterface : INamedType, IHaveMethods, IHaveProperties
    {
        /// <summary>
        /// All events defined in the interface.
        /// </summary>
        IEnumerable<IEvent> Events { get; }

        /// <summary>
        /// All methods defined in the interface.
        /// </summary>
        new IEnumerable<IMethod> Methods { get; }

        /// <summary>
        /// All properties defined in the interface.
        /// </summary>
        new IEnumerable<IProperty> Properties { get; }
    }
}
