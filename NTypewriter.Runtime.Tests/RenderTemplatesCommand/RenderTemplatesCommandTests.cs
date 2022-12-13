using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace NTypewriter.Runtime.Tests.RenderTemplatesCommand
{
    [TestClass]
    public class RenderTemplatesCommandTests
    {
        [TestMethod]
        public async Task HappyPath()
        {
            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(@"..\..\..\..\Tests.Assets.WebApi2022\Tests.Assets.WebApi2022.csproj");
            AdhocWorkspace workspace = analyzer.GetWorkspace(false);
            var project = workspace.CurrentSolution.Projects.Where(x => x.Name == "Tests.Assets.WebApi2022").First();
            var errorList = new IErrorListMock();
            var fileReaderWriter = new IFileReaderWriterMock();
            var output = new IOutputMock();
            var solutionItemsManager = new ISolutionItemsManagerMock();
            var sourceControl = new ISourceControlMock();
            var status = new IStatusMock();

            var cmd = new NTypewriter.Runtime.RenderTemplatesCommand(fileReaderWriter, new IFileSearcherMock(), fileReaderWriter, output, solutionItemsManager, sourceControl, errorList, status);
            var inputTemplate = Path.Combine(Path.GetDirectoryName(project.FilePath)!, "RenderTemplatesCommand_HappyPathTemplate.nt");
            var expectedOutputFile = Path.Combine(Path.GetDirectoryName(project.FilePath)!, "RenderTemplatesCommand_HappyPath.txt");
            var expectedOutput = await fileReaderWriter.Read(expectedOutputFile);
            var input = new List<TemplateToRender>() { new TemplateToRender(inputTemplate, project.FilePath) };
            await cmd.Execute(workspace.CurrentSolution, input);

            var outputContent = output.ToString();
            //var actualOutput = fileReaderWriter.WriteResults[expectedOutputFile];
            Assert.AreEqual(0, fileReaderWriter.WriteResults.Count);    
            Assert.AreEqual(0, errorList.errors.Count);
            //actualOutput.ShouldBe(expectedOutput);
        }
    }
}
