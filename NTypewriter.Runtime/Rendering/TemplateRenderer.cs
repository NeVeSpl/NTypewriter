using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NTypewriter.CodeModel;
using NTypewriter.Editor.Config;
using NTypewriter.Ports;
using NTypewriter.Runtime.Rendering.Internals;

namespace NTypewriter.Runtime.Rendering
{
    public class TemplateRenderer
    {
        private readonly IUserInterfaceErrorListUpdater errorList;
        private readonly IUserInterfaceOutputWriter output;
        private readonly IExpressionCompiler expressionCompiler;

        public TemplateRenderer(IUserInterfaceErrorListUpdater errorList, IUserInterfaceOutputWriter output, IExpressionCompiler expressionCompiler)
        {
            this.errorList = errorList;
            this.output = output;
            this.expressionCompiler = expressionCompiler;
        }


        public async Task<IEnumerable<RenderingResult>> RenderAsync(string templateFilePath, string template, ICodeModel codeModel, IEnumerable<Type> typesThatContainCustomFunctions, EditorConfig editorConfig, EnvironmentVariables environmentVariables)
        {
            errorList.Clear();  
            output.Info("Rendering template");

            var configuration = new NTypewriter.Configuration();
            configuration.AddCustomFunctions(typesThatContainCustomFunctions.ToArray());

            var configAdapter = new EditorConfigAdapterForScriban(editorConfig);

            var dataModels = new Dictionary<string, object>();
            dataModels[VariableNames.Data] = codeModel;
            dataModels["codeModel"] = codeModel;
            dataModels[VariableNames.Config] = configAdapter;
            dataModels[VariableNames.Env] = environmentVariables ?? new EnvironmentVariables();

            var result = await NTypeWriter.Render(template, dataModels, configuration, new ExternalOutputAdapter(output), expressionCompiler);

            output.Info("Used configuration : " + editorConfig.ToString());

            foreach (var message in result.Messages)
            {
                output.Write(message.ToString(), message.Type == MessageType.Error);
                errorList.AddError(templateFilePath, message);
            }
            errorList.Publish();
            if (result.HasErrors)
            {
                throw new RuntimeException("Rendering template failed");
            }

            output.Info("Template rendered successfully");

            var rootDirectory = Path.GetDirectoryName(templateFilePath);
            output.Info($"Root directory : {rootDirectory}");
            var renderedItems = result.Items.Select(x => new RenderingResult(x, rootDirectory)).ToList();

            return renderedItems;
        }
    }
}