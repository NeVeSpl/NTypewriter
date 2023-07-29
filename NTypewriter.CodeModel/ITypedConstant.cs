namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a typed constant.
    /// </summary>
    public interface ITypedConstant
    {
        /// <summary>
        /// The type of the constant.
        /// </summary>
        IType Type { get; }

        /// <summary>
        /// The value for a non-array constant.
        /// </summary>
        object Value { get; }
    }
}