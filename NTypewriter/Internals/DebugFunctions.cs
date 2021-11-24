namespace NTypewriter.Internals
{
    internal static class DebugFunctions
    {
        public static void WriteLine(MainTemplateContext context, string text)
        {
            context.WriteOnExternalOutput(text);
        }

        public static void Throw(string message)
        {
            throw new System.Exception(message);
        }
    }
}