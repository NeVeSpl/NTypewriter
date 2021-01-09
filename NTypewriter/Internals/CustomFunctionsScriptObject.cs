using System;
using System.Collections.Generic;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    public sealed class CustomFunctionsScriptObject : ScriptObject
    {
        private readonly MemberRenamerDelegate MemberRenamer = member => member.Name;
        public static readonly string CustomFunctionsTypeName = "Custom";

        public CustomFunctionsScriptObject(IEnumerable<Type> customFunctions)
        {            
            var scriptObject = ImportAllFunctions(customFunctions);
            Add(CustomFunctionsTypeName, scriptObject);
        }

        private ScriptObject ImportAllFunctions(IEnumerable<Type> customFunctions)
        {
            var scriptObject = new ScriptObject();

            if (customFunctions != null)
            {
                foreach (Type functionType in customFunctions)
                {
                    scriptObject.Import(functionType, renamer: MemberRenamer);
                }
            }
            return scriptObject;
        }
    }
}