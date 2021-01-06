using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a interface.
    /// </summary>
    public interface IInterface : INamedType
    {
        /// <summary>
        /// All events defined in the interface.
        /// </summary>
        IEnumerable<IEvent> Events { get; }

        /// <summary>
        /// All methods defined in the interface.
        /// </summary>
        IEnumerable<IMethod> Methods { get; }

        /// <summary>
        /// All properties defined in the interface.
        /// </summary>
        IEnumerable<IProperty> Properties { get; }
    }
}
