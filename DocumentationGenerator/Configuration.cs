using System;
using System.Linq;
using System.Collections.Generic;
using NTypewriter.CodeModel;
using NTypewriter.Editor.Config;
using NTypewriter.CodeModel.Functions;

namespace DocumentationGenerator
{
    [NTEditorFile]
    class NTEConfig : EditorConfig
    {
        public override IEnumerable<Type> GetTypesThatContainCustomFunctions()
        {
            yield return typeof(NTEConfig);
        }

        public override bool AddGeneratedFilesToVSProject => false;

        public override bool SearchInReferencedProjectsAndAssemblies => true;

        public override IEnumerable<string> GetProjectsToBeSearched()
        {
            yield return "NTypewriter";
        }


        private static HashSet<string> ScribanFunctionSets = new HashSet<string>() 
        { 
            "ArrayFunctions",
            //"DateTimeFunctions",
            "HtmlFunctions", 
            "MathFunctions", 
            "RegexFunctions",
            "StringFunctions",
            "TimeSpanFunctions", 
        };
        private static bool IsScribanFunctionSetAvailble(ISymbolBase type)
        {           
            return ScribanFunctionSets.Contains(type.BareName);
        }
        public static IEnumerable<ISymbolBase> FilterScribanFunctionSets(IEnumerable<ISymbolBase> classes)
        {
            return classes.Where(x => IsScribanFunctionSetAvailble(x)).ToArray();
        }
        public static string ToLiquidId(string text)
        {
            var words = text.SplitIntoSeparateWords();
            var id = String.Join('_', words.Select(x => x.ToLower()));           
            return id;
        }
    }
}