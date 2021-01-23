using NTypewriter.CodeModel;
using NTypewriter.Editor.Config;
using System;
using System.Collections.Generic;
using System.Text;

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

        public override bool SearchInReferencedProjects => true;

        public override IEnumerable<string> GetProjectsToBeSearched()
        {
            yield return "NTypewriter";
        }


        private static HashSet<string> ScribanFunctionSets = new HashSet<string>() 
        { 
            "ArrayFunctions",
            "DateTimeFunctions",
            "HtmlFunctions", 
            "MathFunctions", 
            "RegexFunctions",
            "StringFunctions",
            "TimeSpanFunctions", 
        };
        public static bool IsScribanFunctionSetAvailble(IType type)
        {           
            return ScribanFunctionSets.Contains(type.BareName);
        }
    }
}