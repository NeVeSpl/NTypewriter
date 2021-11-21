using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel
{
    /// <summary>
    /// A program location in source code.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        ///  Returns true if the location represents a specific location in a source code file.
        /// </summary>
        bool IsInSource { get; }    

        /// <summary>
        /// Path, or null if the span represents an invalid value.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the first line number
        /// </summary>
        int StartLinePosition { get; }

        /// <summary>
        /// Gets the last line number
        /// </summary>
        int EndLinePosition { get; }
    }
}
