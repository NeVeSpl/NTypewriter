using System.Collections.Generic;
using Scriban;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    internal sealed class MainTemplateContext : TemplateContext
    {        
        private readonly List<RenderedItem> renderedItems = new List<RenderedItem>();
        private readonly IExternalOutput externalOutput;

        public MainTemplateContext(DataScriptObject dataScriptObject, CustomFunctionsScriptObject customScriptObject, IExternalOutput externalOutput) : base(BuiltinFunctionsScriptObject.Singleton)
        {
            LoopLimit = 66_666;
            MemberRenamer = member => member.Name;
            StrictVariables = true;
            EnableRelaxedMemberAccess = false;

            PushGlobal(customScriptObject);
            PushGlobal(dataScriptObject);
            this.externalOutput = externalOutput;
        }


        public void AddRenderedItem(RenderedItem file)
        {
            renderedItems.Add(file);
        }
        public void WriteOnExternalOutput(string text)
        {
            externalOutput?.Write(text);
        }

        public List<RenderedItem> GetRenderedItems()
        {
            return renderedItems;
        }
    }
}