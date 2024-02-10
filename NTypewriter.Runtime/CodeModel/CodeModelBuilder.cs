using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Roslyn;
using NTypewriter.Runtime.CodeModel.Internals;
using NTypewriter.Runtime.Internals;

namespace NTypewriter.Runtime.CodeModel
{
    public static class CodeModelBuilder
    {
        internal static Task<ICodeModel> Build(SolutionOrCompilation roslynInput, IEnumerable<string> projectsToBeSearchedList, IEnumerable<string> namespacesToBeSearched, bool searchInReferencedProjectsAndAssemblies)
        {
            if (roslynInput.Solution != null)
            {
                return Build(roslynInput.Solution, projectsToBeSearchedList, namespacesToBeSearched, searchInReferencedProjectsAndAssemblies);
            }

            return Task.FromResult(Build(roslynInput.Compilation, namespacesToBeSearched, searchInReferencedProjectsAndAssemblies));
        }

        public static async Task<ICodeModel> Build(Solution solution, IEnumerable<string> projectsToBeSearchedList, IEnumerable<string> namespacesToBeSearched, bool searchInReferencedProjectsAndAssemblies)
        {
            var compositeCodeModel = new CompositeCodeModel();
            var projectGraph = solution.GetProjectDependencyGraph();
            var projectsToBeSearched = new HashSet<string>(projectsToBeSearchedList);

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
                    Compilation compilation = await project.GetCompilationAsync().ConfigureAwait(true);
                    var codeModel = Build(compilation, namespacesToBeSearched, searchInReferencedProjectsAndAssemblies);
                    compositeCodeModel.Add(codeModel);
                }
            }

            return compositeCodeModel;
        }

        public static ICodeModel Build(Compilation compilation, IEnumerable<string> namespacesToBeSearched, bool searchInReferencedProjectsAndAssemblies)
        {
            var codeModelConfig = new CodeModelConfiguration();
            codeModelConfig.OmitSymbolsFromReferencedAssemblies = !searchInReferencedProjectsAndAssemblies;
            if (namespacesToBeSearched != null && namespacesToBeSearched.Any())
            {
                codeModelConfig.FilterByNamespace(namespacesToBeSearched.ToArray());
            }
            
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