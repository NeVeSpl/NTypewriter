using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NTypewriter.Editor.Config;
using NTypewriter.Runtime.CodeModel;
using NTypewriter.Runtime.CodeModel.Internals;
using NTypewriter.Runtime.Configuration;
using NTypewriter.Runtime.Output;
using NTypewriter.Runtime.Rendering;

namespace NTypewriter.Runtime
{
    public class TemplateToRender
    {
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
        private readonly IErrorList errorList;
        private readonly IOutput output;
        private readonly IFileReaderWriter fileReaderWriter;
        private readonly ISourceControl sourceControl;
        private readonly IStatus status;
        private readonly ISolutionItemsManager solutionItemsManager;

        public RenderTemplatesCommand(IErrorList errorList, IOutput output, IFileReaderWriter fileReaderWriter, ISourceControl sourceControl, IStatus status, ISolutionItemsManager solutionItemsManager)
        {
            this.errorList = errorList;
            this.output = output;
            this.fileReaderWriter = fileReaderWriter;
            this.sourceControl = sourceControl;
            this.status = status;
            this.solutionItemsManager = solutionItemsManager;
        }


        public async Task Execute(Solution solution, IList<TemplateToRender> templates)
        {
            await status.Update("Rendering", 0, templates.Count);

            for (int i = 0; i < templates.Count; ++i)
            {
                var template = templates[i];

                output.Info(new String('-', 69));
                output.Info("Rendering started : " + System.IO.Path.GetFileName(template.FilePath));
                await status.Update("Rendering", i + 1, templates.Count);

                var globalConfigurationLoader = new GlobalConfigurationLoader(output);
                var editorConfigSource = await globalConfigurationLoader.LoadConfigurationForGivenProject(solution, template.ProjectPath).ConfigureAwait(false);
                var editorConfig = new EditorConfig(editorConfigSource);             

                var codeModelBuilder = new CodeModelBuilder();
                var codeModel = new LazyCodeModel(() => codeModelBuilder.Build(solution, editorConfig));

                output.Info($"Loading template : {template.FilePath}");
                var templateContent = await fileReaderWriter.Read(template.FilePath).ConfigureAwait(false);

                var templateRenderer = new TemplateRenderer(errorList, output);
                var renderedItems = await templateRenderer.RenderAsync(template.FilePath, templateContent, codeModel, editorConfig).ConfigureAwait(false);

                var fileSaver = new FileSaver(output, sourceControl, fileReaderWriter);
                await fileSaver.Save(renderedItems).ConfigureAwait(false);

                if (editorConfig.AddGeneratedFilesToVSProject)
                {                    
                    output.Info("Updating VisualStudio solution");

                    await solutionItemsManager.UpdateSolution(template.FilePath, renderedItems.Select(x => x.FilePath)).ConfigureAwait(false);
                    output.Info("VisualStudio solution updated successfully");                   
                }

                await status.Update("Rendering succeed");
            }
        }
    }
}