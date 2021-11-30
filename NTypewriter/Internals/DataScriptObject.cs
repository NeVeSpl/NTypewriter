using System.Collections.Generic;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    public sealed class DataScriptObject : ScriptObject
    {
        public static readonly string DataVariableName = "data";
        public static readonly string ConfigVariableName = "config";

        public DataScriptObject(Dictionary<string, object> dataModels)
        {
            foreach (var keyValue in dataModels)
            {
                this[keyValue.Key] = keyValue.Value ?? new ScriptObject();                
            }
        }
    }
}