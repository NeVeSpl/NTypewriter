namespace NTypewriter.CodeModel
{
    /// <summary>
    /// Represents a documentation XML param tag.
    /// </summary>
    public interface IDocumentationCommentXmlParam
    {
        /// <summary>
        /// The value of the name attribute
        /// </summary>
        string Name { get;  }

        /// <summary>
        /// The content of the param tag
        /// </summary>
        string Value { get; }
    }
}