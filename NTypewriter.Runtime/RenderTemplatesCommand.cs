using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime.CodeModel;
using NTypewriter.Runtime.CodeModel.Internals;
using NTypewriter.Runtime.Internals;
using NTypewriter.Runtime.Output;
using NTypewriter.Runtime.Rendering;
using NTypewriter.Runtime.UserCode;

namespace NTypewriter.Runtime
{
    public class TemplateToRender
    {
        public string Content { get; set; }
        public string FilePath { get; set; }
        public string ProjectPath { get; set; }


        public TemplateToRender(string filePath, string projectPath)
        {
            FilePath = filePath;
            ProjectPath = projectPath;
        }
    }  


    public class RenderTemplatesCommand
    {
        private readonly IGeneratedFileReaderWriter generatedFileReaderWriter;
        private readonly ISolutionItemsManager solutionItemsManager;
        private readonly ISourceControl sourceControl;
        private readonly ITemplateContentLoader templateContentLoader;
        private readonly IUserCodeSearcher userCodeSearcher;
        private readonly IUserInterfaceErrorListUpdater uiErrorList; 
        private readonly IUserInterfaceOutputWriter uiOutput;        
        private readonly IUserInterfaceStatusUpdater uiStatus;        


        public RenderTemplatesCommand(ITemplateContentLoader templateContentLoader, 
                                      IUserCodeSearcher fileSearcher,
                                      IGeneratedFileReaderWriter generatedFileReaderWriter,
                                      IUserInterfaceOutputWriter output = null,
                                      ISolutionItemsManager solutionItemsManager = null,
                                      ISourceControl sourceControl = null,
                                      IUserInterfaceErrorListUpdater errorList = null,
                                      IUserInterfaceStatusUpdater status = null)
        {
            this.templateContentLoader = templateContentLoader;
            this.userCodeSearcher = fileSearcher;
            this.generatedFileReaderWriter = generatedFileReaderWriter;
            this.uiErrorList = errorList ?? NullObject.Singleton;
            this.uiOutput = output ?? NullObject.Singleton;
            this.solutionItemsManager = solutionItemsManager;
            this.sourceControl = sourceControl ?? NullObject.Singleton;
            this.uiStatus = status ?? NullObject.Singleton;
        }


        public async Task Execute(Solution solution, IList<TemplateToRender> templates, EnvironmentVariables environmentVariables = null)
        {
            await uiStatus.Update("Rendering", 0, templates.Count);

            for (int i = 0; i < templates.Count; ++i)
            {
                var template = templates[i];

                uiOutput.Info(new String('-', 69));
                uiOutput.Info("Rendering started : " + System.IO.Path.GetFileName(template.FilePath));
                await uiStatus.Update("Rendering", i + 1, templates.Count);

                var userCodeLoader = new UserCodeLoader(uiOutput, userCodeSearcher);
                var userCode = await userCodeLoader.LoadUserCodeForGivenProject(solution, template.ProjectPath).ConfigureAwait(false);
                var editorConfig = new EditorConfig(userCode.Config);             

                var codeModelBuilder = new CodeModelBuilder();
                var codeModel = new LazyCodeModel(() => codeModelBuilder.Build(solution, editorConfig));

                uiOutput.Info($"Loading template : {template.FilePath}");
                var templateContent = template.Content ?? await templateContentLoader.Read(template.FilePath).ConfigureAwait(false);

                var templateRenderer = new TemplateRenderer(uiErrorList, uiOutput);
                var renderedItems = await templateRenderer.RenderAsync(template.FilePath, templateContent, codeModel, userCode.TypesThatMayContainCustomFunctions, editorConfig, environmentVariables).ConfigureAwait(false);

                var fileSaver = new FileSaver(uiOutput, sourceControl, generatedFileReaderWriter);
                await fileSaver.Save(renderedItems).ConfigureAwait(false);

                if ((solutionItemsManager != null) && (editorConfig.AddGeneratedFilesToVSProject))
                {                    
                    uiOutput.Info("Updating VisualStudio solution");
                    await solutionItemsManager.UpdateSolution(template.FilePath, renderedItems.Select(x => x.FilePath)).ConfigureAwait(false);
                    uiOutput.Info("VisualStudio solution updated successfully");                   
                }

                await uiStatus.Update("Rendering succeed");
            }
        }
    }
}