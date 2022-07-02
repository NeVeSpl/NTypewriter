using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a property.
    /// </summary>
    public interface IProperty : ISymbolBase
    {
        /// <summary>
        /// Determines if the property is really an indexer.
        /// </summary>
        bool IsIndexer { get; }

        /// <summary>
        /// Determines if the property is a write-only property
        /// </summary>
        bool IsWriteOnly { get; }

        /// <summary>
        /// Determines if the property is a read-only property
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Determines if the property is sealed
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// The type of the property.
        /// </summary>
        IType Type { get; }
    }
}