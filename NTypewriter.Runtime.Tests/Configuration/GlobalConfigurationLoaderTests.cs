using System.Linq;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTypewriter.Runtime.UserCode;
using Shouldly;

namespace NTypewriter.Runtime.Tests
{
    [TestClass]
    public class GlobalConfigurationLoaderTests
    {
        [TestMethod]
        public void LoadConfigurationForGivenProjectShouldReturnDefinedConfig()
        {
            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(@"..\..\..\..\Tests.Assets.WebApi2022\Tests.Assets.WebApi2022.csproj");
            AdhocWorkspace workspace = analyzer.GetWorkspace(false);
            var project = workspace.CurrentSolution.Projects.Where(x => x.Name == "Tests.Assets.WebApi2022").First();
            var output = new IOutputMock();          

            var userCode = UserCodeLoader.LoadUserCodeFromGivenProject(project.FilePath, new IFileSearcherMock(), output);
            
            userCode.GlobalConfig.AddGeneratedFilesToVSProject.ShouldBeFalse();
            userCode.GlobalConfig.ProjectsToBeSearched.ShouldBe(new[] { "Tests.Assets.WebApi2022" });
            userCode.TypesThatMayContainCustomFunctions.Select(x => x.FullName).ShouldBe(new[] { "Tests.Assets.WebApi2022.CustomFunctions" }, ignoreOrder: true);
        }
    }
}