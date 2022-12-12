namespace NTypewriter.Internals.Functions
{
    /// <summary>
    /// Set of functions that can be helpful during bug hunting 
    /// </summary>
    internal static class DebugFunctions
    {
        /// <summary>
        /// Write text on NTypewriter output window
        /// </summary>       
        public static void WriteLine(MainTemplateContext context, string text)
        {
            context.WriteOnExternalOutput(text);
        }

        /// <summary>
        /// Throws exception and stops rendering
        /// </summary>
        public static void Throw(string message)
        {
            throw new System.Exception(message);
        }
    }
}