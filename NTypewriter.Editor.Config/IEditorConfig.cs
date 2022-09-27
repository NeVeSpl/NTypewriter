using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IEditorConfig : IConstrainSearchedNamespaces,
                                     IConstrainSearchedProjects,
                                     ISearchInReferencedProjectsAndAssemblies
    {
        bool AddGeneratedFilesToVSProject { get; set; }
        bool RenderWhenTemplateIsSaved { get; set; }
        bool RenderWhenProjectBuildIsDone { get; set; }
    }
}
