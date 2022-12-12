using System.Collections.Generic;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    internal sealed class DataScriptObject : ScriptObject
    { 
        public DataScriptObject(Dictionary<string, object> dataModels)
        {
            foreach (var keyValue in dataModels)
            {
                this[keyValue.Key] = keyValue.Value ?? new ScriptObject();                
            }
        }
    }
}