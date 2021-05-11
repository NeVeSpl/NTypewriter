using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.Editor.Config
{
    public class EditorConfig : IEditorConfig
    {   
        public virtual bool SearchInReferencedProjectsAndAssemblies { get; set; } = false;

        public virtual bool AddGeneratedFilesToVSProject { get; set; } = true;

        public virtual IEnumerable<string> NamespacesToBeSearched { get; set; } = Enumerable.Empty<string>();

        public virtual IEnumerable<string> ProjectsToBeSearched { get; set; } = Enumerable.Empty<string>();

        public virtual IEnumerable<Type> TypesThatContainCustomFunctions { get; set; } = Enumerable.Empty<Type>();


        public EditorConfig()
        {

        }
        public EditorConfig(IEditorConfig editorConfig)
        {
            SearchInReferencedProjectsAndAssemblies = editorConfig.SearchInReferencedProjectsAndAssemblies;
            AddGeneratedFilesToVSProject = editorConfig.AddGeneratedFilesToVSProject;
            NamespacesToBeSearched = editorConfig.NamespacesToBeSearched;
            ProjectsToBeSearched = editorConfig.ProjectsToBeSearched;
            TypesThatContainCustomFunctions = editorConfig.TypesThatContainCustomFunctions;
        }
    }
}