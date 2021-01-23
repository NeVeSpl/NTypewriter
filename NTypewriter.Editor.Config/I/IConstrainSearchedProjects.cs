using System;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public interface IConstrainSearchedProjects
    {
        IEnumerable<string> GetProjectsToBeSearched();
    }
}
