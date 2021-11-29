using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface ILocalConfig : IConstrainSearchedNamespaces,
                                    IConstrainSearchedProjects,
                                    ISearchInReferencedProjectsAndAssemblies
    {
        bool AddGeneratedFilesToVSProject { get; }
    }
}
