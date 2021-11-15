using System;
using System.Linq;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace Tests.Assets.WebApi2022
{
    [NTEditorFile]
    class SampleConfig : EditorConfig
    {


        public SampleConfig()
        {
            SearchInReferencedProjectsAndAssemblies = true;
            AddGeneratedFilesToVSProject = false;
            RenderWhenTemplateIsSaved = true;
            NamespacesToBeSearched = new[] { "Tests.Assets.WebApi2022" };
            ProjectsToBeSearched = new[] { "Tests.Assets.WebApi2022" };
            TypesThatContainCustomFunctions = new[] { typeof(SampleConfig) };
        }
    }
}