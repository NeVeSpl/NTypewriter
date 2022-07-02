using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public interface IEvent : ISymbolBase
    {
        /// <summary>
        /// Determines if the event is sealed
        /// </summary>
        bool IsSealed { get; }



        /// <summary>
        /// The type of the event.
        /// </summary>
        IType Type { get; }       
    }
}