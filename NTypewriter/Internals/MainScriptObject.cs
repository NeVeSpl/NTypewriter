using Scriban.Runtime;

namespace NTypewriter.Internals
{
    public sealed class MainScriptObject : ScriptObject
    {
        public static readonly string DataVariableName = "data";


        public MainScriptObject(object data)
        {
            this[DataVariableName] = data;
        }
    }
}