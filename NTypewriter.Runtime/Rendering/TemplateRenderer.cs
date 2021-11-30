using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTypewriter.CodeModel;
using NTypewriter.Editor.Config;
using NTypewriter.Internals;
using NTypewriter.Runtime.Rendering.Internals;

namespace NTypewriter.Runtime.Rendering
{
    public class TemplateRenderer
    {
        private readonly IErrorList errorList;
        private readonly IOutput output;


        public TemplateRenderer(IErrorList errorList, IOutput output)
        {
            this.errorList = errorList;
            this.output = output;
        }


        public async Task<IEnumerable<RenderingResult>> RenderAsync(string templateFilePath, string template, ICodeModel codeModel, IEnumerable<Type> typesThatContainCustomFunctions, EditorConfig editorConfig)
        {
            errorList.Clear();  
            output.Info("Rendering template");

            var configuration = new NTypewriter.Configuration();
            configuration.AddCustomFunctions(typesThatContainCustomFunctions.ToArray());

            var configAdapter = new EditorConfigAdapterForScriban(editorConfig);

            var dataModels = new Dictionary<string, object>();
            dataModels[DataScriptObject.DataVariableName] = codeModel;
            dataModels["codeModel"] = codeModel;
            dataModels[DataScriptObject.ConfigVariableName] = configAdapter;

            var result = await NTypeWriter.Render(template, dataModels, configuration, new ExternalOutputAdapter(output));

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
            output.Info($"Root directory : {rootDirectory})");
            var renderedItems = result.Items.Select(x => new RenderingResult(x, rootDirectory)).ToList();

            return renderedItems;
        }
    }
}