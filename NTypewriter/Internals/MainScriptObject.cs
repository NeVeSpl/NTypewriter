using Scriban.Runtime;

namespace NTypewriter.Internals
{
    public sealed class MainScriptObject : ScriptObject
    {
        public static readonly string DataVariableName = "data";
        public static readonly string ConfigVariableName = "config";

        public MainScriptObject(object data, object config)
        {
            this[DataVariableName] = data;
            this[ConfigVariableName] = config ?? new ScriptObject();
        }
    }
}