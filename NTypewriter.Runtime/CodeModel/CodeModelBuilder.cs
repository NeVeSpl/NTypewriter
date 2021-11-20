using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Roslyn;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime.CodeModel.Internals;

namespace NTypewriter.Runtime.CodeModel
{
    public class CodeModelBuilder
    {
        public async Task<ICodeModel> Build(Solution solution, IEditorConfig editorConfig)
        {
            var compositeCodeModel = new CompositeCodeModel();
            var projectGraph = solution.GetProjectDependencyGraph();
            var projectsToBeSearched = new HashSet<string>(editorConfig.ProjectsToBeSearched);

            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                Project project = solution.GetProject(projectId);
                bool isProjectOnTheList = projectsToBeSearched.Contains(project.Name);
                if (projectsToBeSearched.Any() == false)
                {
                    isProjectOnTheList = true;
                }

                if ((project.SupportsCompilation) && (isProjectOnTheList))
                {
                    var codeModel = await CreateCodeModel(project, editorConfig.NamespacesToBeSearched, editorConfig.SearchInReferencedProjectsAndAssemblies).ConfigureAwait(true);
                    compositeCodeModel.Add(codeModel);
                }
            }

            return compositeCodeModel;
        }

        private async Task<NTypewriter.CodeModel.Roslyn.CodeModel> CreateCodeModel(Project project, IEnumerable<string> namespacesToBeSearched, bool searchInReferencedProjectsAndAssemblies)
        {
            var codeModelConfig = new CodeModelConfiguration();
            codeModelConfig.OmitSymbolsFromReferencedAssemblies = !searchInReferencedProjectsAndAssemblies;
            if (namespacesToBeSearched != null && namespacesToBeSearched.Any())
            {
                codeModelConfig.FilterByNamespace(namespacesToBeSearched.ToArray());
            }

            var compilation = await project.GetCompilationAsync().ConfigureAwait(true);
            var diagnostics = compilation.GetDiagnostics();
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Severity != DiagnosticSeverity.Hidden)
                {
                    // that is so nice place for breakpoint in moment of need
                }
            }

            var codeModel = new NTypewriter.CodeModel.Roslyn.CodeModel(compilation, codeModelConfig);
            return codeModel;
        }
    }
}