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
        /// The type of the event.
        /// </summary>
        IType Type { get; }       
    }
}