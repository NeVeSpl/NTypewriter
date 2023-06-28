using System;
using System.Linq;
using System.Collections.Generic;
using NTypewriter.Editor.Config;

namespace Tests.Assets.WebApi2022
{   
    class SampleConfig : EditorConfig
    {
        public SampleConfig()
        {
            SearchInReferencedProjectsAndAssemblies = true;
            AddGeneratedFilesToVSProject = false;
            RenderWhenTemplateIsSaved = true;
            NamespacesToBeSearched = new[] { "Tests.Assets.WebApi2022" };
            ProjectsToBeSearched = new[] { "Tests.Assets.WebApi2022" };           
        }
    }

    public static class CustomFunctions { }

    #region Non valid custom function type (only non generic public static class)
    public class PublicNonStaticClass { }
    static class NonPublicStaticClass { }
    public static class SomeGenericPublicStaticClass<T> { }
    #endregion
}