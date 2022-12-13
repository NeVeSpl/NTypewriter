using System.Linq;
using NTypewriter.Editor.Config;
using Scriban.Runtime;

namespace NTypewriter.Runtime.Rendering.Internals
{
    internal class EditorConfigAdapterForScriban
    {
        public readonly EditorConfig editorConfig;


        public EditorConfigAdapterForScriban(EditorConfig editorConfig)
        {
            this.editorConfig = editorConfig;
        }

        public bool RenderWhenTemplateIsSaved
        {
            get => editorConfig.RenderWhenTemplateIsSaved;
            set => editorConfig.RenderWhenTemplateIsSaved = value;
        }
        public bool RenderWhenProjectBuildIsDone
        {
            get => editorConfig.RenderWhenProjectBuildIsDone;
            set => editorConfig.RenderWhenProjectBuildIsDone = value;
        }
        public bool SearchInReferencedProjectsAndAssemblies
        {
            get => editorConfig.SearchInReferencedProjectsAndAssemblies;
            set => editorConfig.SearchInReferencedProjectsAndAssemblies = value;
        }
        public bool AddGeneratedFilesToVSProject
        {
            get => editorConfig.AddGeneratedFilesToVSProject;
            set => editorConfig.AddGeneratedFilesToVSProject = value;
        }
        public ScriptArray NamespacesToBeSearched
        {
            get
            {
                return new ScriptArray(editorConfig.NamespacesToBeSearched);
            }
            set
            {
                editorConfig.NamespacesToBeSearched = value.OfType<string>().ToList();
            }
        }
        public ScriptArray ProjectsToBeSearched
        {
            get
            {
                return new ScriptArray(editorConfig.ProjectsToBeSearched);
            }
            set
            {
                editorConfig.ProjectsToBeSearched = value.OfType<string>().ToList();
            }
        }


        public override string ToString()
        {
            return editorConfig.ToString();
        }
    }
}