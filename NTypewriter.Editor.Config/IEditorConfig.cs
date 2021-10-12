using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IEditorConfig :
        IConstrainSearchedNamespaces,
        IConstrainSearchedProjects,
        IHaveCustomFunctions,
        ISearchInReferencedProjectsAndAssemblies

    {
        bool AddGeneratedFilesToVSProject { get; }
        bool RenderWhenTemplateIsSaved { get; }
    }
}
