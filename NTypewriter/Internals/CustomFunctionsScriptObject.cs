using System;
using System.Collections.Generic;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    internal sealed class CustomFunctionsScriptObject : ScriptObject
    {
        private readonly MemberRenamerDelegate MemberRenamer = member => member.Name;
        

        public CustomFunctionsScriptObject(IEnumerable<Type> customFunctions)
        {            
            var scriptObject = ImportAllFunctions(customFunctions);
            Add(VariableNames.CustomFunctions, scriptObject);
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