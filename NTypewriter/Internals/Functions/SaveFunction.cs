namespace NTypewriter.Internals.Functions
{
    internal static class SaveFunction
    {
        public static void Save(MainTemplateContext context, string content, string name)
        {
            context.AddRenderedItem(new RenderedItem(name, content));
        }
    }
}