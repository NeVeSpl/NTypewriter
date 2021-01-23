using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IEditorConfig :
        IConstrainSearchedNamespaces,
        IConstrainSearchedProjects,
        IHaveCustomFunctions,
        ISearchInReferencedProjects

    {
       bool AddGeneratedFilesToVSProject { get; }
    }
}
