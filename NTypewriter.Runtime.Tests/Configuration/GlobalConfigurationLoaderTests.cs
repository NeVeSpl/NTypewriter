using System.Linq;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.Runtime.Configuration;
using Shouldly;

namespace NTypewriter.Runtime.Tests
{
    [TestClass]
    public class GlobalConfigurationLoaderTests
    {
        [TestMethod]
        public async Task LoadConfigurationForGivenProjectShouldReturnDefinedConfig()
        {
            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(@"..\..\..\..\Tests.Assets.WebApi2022\Tests.Assets.WebApi2022.csproj");
            AdhocWorkspace workspace = analyzer.GetWorkspace(false);
            var project = workspace.CurrentSolution.Projects.Where(x => x.Name == "Tests.Assets.WebApi2022").First();
            var output = new IOutputMock();
            var loader = new GlobalConfigurationLoader(output);

            var config = await loader.LoadConfigurationForGivenProject(workspace.CurrentSolution, project.FilePath);

            Assert.AreEqual(false, config.AddGeneratedFilesToVSProject);
            config.ProjectsToBeSearched.ShouldBe(new[] { "Tests.Assets.WebApi2022" });
        }
    }
}