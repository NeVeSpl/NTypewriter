using System.Collections.Generic;
using System.Threading.Tasks;
using NTypewriter.Internals;
using NTypewriter.Ports;
using Scriban;
using Scriban.Syntax;

namespace NTypewriter
{
    public class NTypeWriter
    {
        public static async Task<Result> Render(string template, object dataModel, Configuration configuration = null, IExternalOutput externalOutput = null, IExpressionCompiler expressionCompiler = null)
        {
            return await Render(template, new Dictionary<string, object>() { [VariableNames.Data] = dataModel }, configuration, externalOutput, expressionCompiler);
        }


        public static async Task<Result> Render(string template, Dictionary<string, object> dataModels, Configuration configuration = null, IExternalOutput externalOutput = null, IExpressionCompiler expressionCompiler = null)
        {
            var result = new Result();
            var scribanTemplate = Template.Parse(template);

            result.AddMsgFromScribanTemplateParsing(scribanTemplate.Messages);

            if (result.HasErrors)
            {
                return result;            
            }          
           
            var dataScriptObject = new DataScriptObject(dataModels);
            var userScriptObject = new CustomFunctionsScriptObject(configuration?.GetTypesWithCustomFuntions());
            var context = new MainTemplateContext(dataScriptObject, userScriptObject, externalOutput, expressionCompiler);

            try
            {
                result.SetOutput(scribanTemplate.Render(context));
            }
            catch (ScriptRuntimeException exception)
            {
                result.AddMsgFromScribanException(exception);
            }

            if (result.HasErrors)
            {
                return result;
            }

            result.AddRenderedItems(context.GetRenderedItems());

            return result;
        }
    }
}