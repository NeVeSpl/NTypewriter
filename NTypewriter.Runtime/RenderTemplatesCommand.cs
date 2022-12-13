using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime.CodeModel;
using NTypewriter.Runtime.CodeModel.Internals;
using NTypewriter.Runtime.Internals;
using NTypewriter.Runtime.Rendering;
using NTypewriter.Runtime.UserCode;

namespace NTypewriter.Runtime
{
    public class TemplateToRender
    {
        public string Content { get; set; }
        public string FilePath { get;  }
        public string ContainingProjectPath { get; set; }


        public TemplateToRender(string filePath, string containingProjectPath)
        {
            FilePath = filePath;
            ContainingProjectPath = containingProjectPath;
        }
    }  


    public class RenderTemplatesCommand
    {
        private readonly IGeneratedFileReaderWriter generatedFileReaderWriter;
        private readonly ISolutionItemsManager solutionItemsManager;
        private readonly ISourceControl sourceControl;
        private readonly ITemplateContentLoader templateContentLoader;
        private readonly IUserCodeProvider userCodeProvider;
        private readonly IUserInterfaceErrorListUpdater uiErrorList; 
        private readonly IUserInterfaceOutputWriter uiOutput;        
        private readonly IUserInterfaceStatusUpdater uiStatus;        


        public RenderTemplatesCommand(ITemplateContentLoader templateContentLoader, 
                                      IUserCodeProvider userCodeProvider,
                                      IGeneratedFileReaderWriter generatedFileReaderWriter,
                                      IUserInterfaceOutputWriter uiOutput = null,
                                      ISolutionItemsManager solutionItemsManager = null,
                                      ISourceControl sourceControl = null,
                                      IUserInterfaceErrorListUpdater uiErrorList = null,
                                      IUserInterfaceStatusUpdater uiStatus = null)
        {
            this.templateContentLoader = templateContentLoader;
            this.userCodeProvider = userCodeProvider;
            this.generatedFileReaderWriter = generatedFileReaderWriter;
            this.uiErrorList = uiErrorList ?? NullObject.Singleton;
            this.uiOutput = uiOutput ?? NullObject.Singleton;
            this.solutionItemsManager = solutionItemsManager;
            this.sourceControl = sourceControl ?? NullObject.Singleton;
            this.uiStatus = uiStatus ?? NullObject.Singleton;
        }


        public Task Execute(Solution solution, IList<TemplateToRender> templates, EnvironmentVariables environmentVariables = null)
        {
            return Execute(new SolutionOrCompilation(solution), templates, environmentVariables);
        }
        public Task Execute(Compilation compilation, IList<TemplateToRender> templates, EnvironmentVariables environmentVariables = null)
        {
            return Execute(new SolutionOrCompilation(compilation), templates, environmentVariables);
        }    


        private async Task Execute(SolutionOrCompilation roslynInput, IList<TemplateToRender> templates, EnvironmentVariables environmentVariables = null)
        {
            await uiStatus.Update("Rendering", 0, templates.Count);

            for (int i = 0; i < templates.Count; ++i)
            {
                var template = templates[i];

                uiOutput.Info(new String('-', 69));
                uiOutput.Info("Rendering started : " + System.IO.Path.GetFileName(template.FilePath));
                await uiStatus.Update("Rendering", i + 1, templates.Count);

                await UsedAssemblyVersionChecker.Check(roslynInput, template.ContainingProjectPath, uiOutput).ConfigureAwait(false);
            
                var userInput = UserCodeLoader.LoadUserCodeFromGivenProject(template.ContainingProjectPath, userCodeProvider, uiOutput);
                var editorConfig = new EditorConfig(userInput.GlobalConfig);  
             
                var codeModel = new LazyCodeModel(() => CodeModelBuilder.Build(roslynInput, editorConfig.ProjectsToBeSearched, editorConfig.NamespacesToBeSearched, editorConfig.SearchInReferencedProjectsAndAssemblies));

                uiOutput.Info($"Loading template : {template.FilePath}");
                var templateContent = template.Content ?? await templateContentLoader.Read(template.FilePath).ConfigureAwait(false);

                var templateRenderer = new TemplateRenderer(uiErrorList, uiOutput);
                var renderedItems = await templateRenderer.RenderAsync(template.FilePath, templateContent, codeModel, userInput.TypesThatMayContainCustomFunctions, editorConfig, environmentVariables).ConfigureAwait(false);
                
                await GeneratedFileManager.SaveChanges(renderedItems, generatedFileReaderWriter, uiOutput, sourceControl).ConfigureAwait(false);

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