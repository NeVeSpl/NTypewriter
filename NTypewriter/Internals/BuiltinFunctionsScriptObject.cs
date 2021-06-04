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
            SetValue("empty", EmptyScriptObject.Default, true);
            this["Array"] = CreateScriptObject(typeof(ArrayFunctions));
            this["Date"] = CreateScriptObject(typeof(DateTimeFunctions));           
            this["Html"] = CreateScriptObject(typeof(HtmlFunctions)); 
            this["Math"] = CreateScriptObject(typeof(MathFunctions)); 
            //this["object"] = CreateScriptObject(typeof(ObjectFunctions)); 
            this["Regex"] = CreateScriptObject(typeof(RegexFunctions));
            this["Parameters"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.ParametersFunctions));
            this["String"] = CreateScriptObject(typeof(StringFunctions), typeof(global::NTypewriter.CodeModel.Functions.StringFunctions)); 
            this["Timespan"] = CreateScriptObject(typeof(TimeSpanFunctions));
            this["Action"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.ActionFunctions));
            this["Type"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.TypeFunctions));
            this["SearchIn"] = CreateScriptObjectFromEnum(typeof(global::NTypewriter.CodeModel.Functions.SearchIn));
            this["Types"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.TypesFunctions));
            this["Symbol"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.SymbolFunctions));
            this["Symbols"] = CreateScriptObject(typeof(global::NTypewriter.CodeModel.Functions.SymbolsFunctions));           
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

        private object CreateScriptObjectFromEnum(Type type)
        {          
            var scriptObject = new ScriptObject();

            foreach(var value in Enum.GetValues(type))
            {
                var key = value.ToString();
                scriptObject[key] = (int)value;
            }

            return scriptObject;
        }
    }
}