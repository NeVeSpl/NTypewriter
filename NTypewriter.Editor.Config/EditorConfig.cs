using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public class EditorConfig : IEditorConfig
    {   
        public virtual bool SearchInReferencedProjects => false;

        public virtual bool AddGeneratedFilesToVSProject => true;

        public virtual IEnumerable<string> GetNamespacesToBeSearched()
        {
            return Enumerable.Empty<string>();
        }

        public virtual IEnumerable<string> GetProjectsToBeSearched()
        {
            return Enumerable.Empty<string>();
        }

        public virtual IEnumerable<Type> GetTypesThatContainCustomFunctions()
        {
            return Enumerable.Empty<Type>();
        }
    }
}