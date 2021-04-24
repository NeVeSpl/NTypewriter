using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.MSBuild;
using NTypewriter.CodeModel.Roslyn;

namespace NTypewriter.CodeModel.Functions.Tests
{
    public class BaseFixture
    {
        public static async Task<global::NTypewriter.CodeModel.Roslyn.CodeModel> CreateCodeModelFromProject(CodeModelConfiguration config, string projectName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var srcPath = Path.Combine(Path.GetDirectoryName(assembly.Location), @$"..\..\..\..\{projectName}\{projectName}.csproj");


            var project = await LoadProject(srcPath);
            var compilation = await project.GetCompilationAsync();
            var codeModel = new global::NTypewriter.CodeModel.Roslyn.CodeModel(compilation, config);
            return codeModel;

        }


        private static async Task<Project> LoadProject(string path)
        {
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }
            if (MefHostServices.DefaultHost == null)
            {
                var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            }

            var workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (x, y) =>
            {

            };

            var project = await workspace.OpenProjectAsync(path);

            var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            var runtimeDllPath = Path.Combine(runtimeDir, "System.Runtime.dll");
            var netcoreDllPath = Path.Combine(runtimeDir.Replace("NETCore", "AspNetCore"), "Microsoft.AspNetCore.Mvc.Core.dll");

            var references = new List<MetadataReference>()
            {
                 MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                 MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location),
                 MetadataReference.CreateFromFile(runtimeDllPath),
                 MetadataReference.CreateFromFile(netcoreDllPath)
            };

            var projectId = project.Id;

            foreach (var item in workspace.CurrentSolution.Projects)
            {
                project = project.Solution.GetProject(item.Id);
                project = project.AddMetadataReferences(ImmutableList.Create<MetadataReference>(references.ToArray()));
            }

            project = project.Solution.GetProject(projectId);
            return project;
        }


        protected (string template, string expectedResult) LoadResources(string typeName, string functionName)
        {
            var path = $"{typeName}.{functionName}";
            string template = LoadResource(path, "inputTemplate.nt");            
            string expectedResult = LoadResource(path, "expectedResult.txt");        
            return (template, expectedResult);
        }

        private string LoadResource(string folders, string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"NTypewriter.CodeModel.Functions.Tests.{folders}.{fileName}";

            string result = System.String.Empty;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream is not null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            return result;
        }

        protected string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}