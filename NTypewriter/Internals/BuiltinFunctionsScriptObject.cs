using System;
using Scriban.Functions;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    public sealed class BuiltinFunctionsScriptObject : ScriptObject
    {
        private readonly MemberRenamerDelegate MemberRenamer = member => member.Name;
        public static readonly BuiltinFunctionsScriptObject Singleton = new BuiltinFunctionsScriptObject();

        private BuiltinFunctionsScriptObject()
        {
            this["Array"] = CreateScriptObject(typeof(ArrayFunctions));
            //this["empty"] = CreateScriptObject(typeof(EmptyScriptObject)); 
            this["Html"] = CreateScriptObject(typeof(HtmlFunctions)); 
            this["Math"] = CreateScriptObject(typeof(MathFunctions)); 
            //this["object"] = CreateScriptObject(typeof(ObjectFunctions)); 
            this["Regex"] = CreateScriptObject(typeof(RegexFunctions)); 
            this["String"] = CreateScriptObject(typeof(StringFunctions), typeof(NTypewriter.CodeModel.Functions.StringFunctions)); 
            this["Timespan"] = CreateScriptObject(typeof(TimeSpanFunctions));
            this["Method"] = CreateScriptObject(typeof(NTypewriter.CodeModel.Functions.MethodFunctions));
            this["Type"] = CreateScriptObject(typeof(NTypewriter.CodeModel.Functions.TypeFunctions));
            this["Types"] = CreateScriptObject(typeof(NTypewriter.CodeModel.Functions.TypesFunctions));
            this["Symbols"] = CreateScriptObject(typeof(NTypewriter.CodeModel.Functions.SymbolsFunctions));
            this.Import(typeof(SaveFunction), renamer: MemberRenamer);
        }

        private ScriptObject CreateScriptObject(params Type[] types)
        {
            var scriptObject = new ScriptObject();

            foreach (var type in types)
            {
                scriptObject.Import(type, renamer: MemberRenamer);
            }

            return scriptObject;
        }
    }
}