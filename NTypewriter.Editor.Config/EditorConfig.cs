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
        
        public virtual bool RenderWhenTemplateIsSaved { get; set; } = false;

        public virtual bool RenderWhenProjectBuildIsDone { get; set; } = false;

        public virtual IEnumerable<string> NamespacesToBeSearched { get; set; } = Enumerable.Empty<string>();

        public virtual IEnumerable<string> ProjectsToBeSearched { get; set; } = Enumerable.Empty<string>();

        [Obsolete("This property is obsolete, and will be removed in the future. Classes that contain custom funtions are now detected automaticly, there is no need anymore to list them here, you can remove this property.")]
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
            RenderWhenTemplateIsSaved = editorConfig.RenderWhenTemplateIsSaved;
            RenderWhenProjectBuildIsDone = editorConfig.RenderWhenProjectBuildIsDone;
        }

        public override string ToString()
        {
            return $"{nameof(SearchInReferencedProjectsAndAssemblies)}: {SearchInReferencedProjectsAndAssemblies}; "+
                   $"{nameof(AddGeneratedFilesToVSProject)}: {AddGeneratedFilesToVSProject}; "+
                   $"{nameof(RenderWhenTemplateIsSaved)}: {RenderWhenTemplateIsSaved}; " +
                   $"{nameof(RenderWhenProjectBuildIsDone)}: {RenderWhenProjectBuildIsDone}; " +
                   $"{nameof(NamespacesToBeSearched)}: [{String.Join(", ", NamespacesToBeSearched)}]; " +
                   $"{nameof(ProjectsToBeSearched)}: [{String.Join(", ", ProjectsToBeSearched)}]; "                  
                   ;
        }
    }
}