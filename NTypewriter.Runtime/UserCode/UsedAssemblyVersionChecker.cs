using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.CodeModel;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime.Internals;

namespace NTypewriter.Runtime.UserCode
{
    internal static class UsedAssemblyVersionChecker
    {
        public static async Task Check(SolutionOrCompilation roslynInput, string projectPath, IUserInterfaceOutputWriter output)
        {
            Compilation compilation = roslynInput.Compilation;

            if (roslynInput.Solution != null)
            {
                var project = roslynInput.Solution.Projects.FirstOrDefault(x => x.FilePath == projectPath);
                if (project == null)
                {
                    return;
                }
                compilation = await project.GetCompilationAsync();                
            }

            CheckUsedAssemblyVersions(compilation, output);
        }


        private static void CheckUsedAssemblyVersions(Compilation compilation, IUserInterfaceOutputWriter output)
        {            
            ShowWarningIfAssemblyVersionDoesNotMatch(compilation, typeof(EditorConfig), output);
            ShowWarningIfAssemblyVersionDoesNotMatch(compilation, typeof(ICodeModel), output);
        }
        private static void ShowWarningIfAssemblyVersionDoesNotMatch(Compilation compilation, Type sampleType, IUserInterfaceOutputWriter output)
        {
            var runtimeAssemblyName = sampleType.Assembly.GetName();
            var projectAssembly = compilation?.ReferencedAssemblyNames.Where(x => x.Name == runtimeAssemblyName.Name).FirstOrDefault();

            if ((projectAssembly != null) && (projectAssembly.Version != runtimeAssemblyName.Version))
            {
                output.Info($"You are using different version ({projectAssembly.Version}) of {projectAssembly.Name} than version used by runtime ({runtimeAssemblyName.Version})");
            }
        }
    }
}