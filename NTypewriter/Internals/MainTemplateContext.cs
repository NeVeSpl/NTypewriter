using System.Collections.Generic;
using Scriban;
using Scriban.Runtime;

namespace NTypewriter.Internals
{
    internal sealed class MainTemplateContext : TemplateContext
    {        
        private readonly List<RenderedItem> renderedItems = new List<RenderedItem>();


        public MainTemplateContext(MainScriptObject mainScriptObject, CustomFunctionsScriptObject customScriptObject) : base(BuiltinFunctionsScriptObject.Singleton)
        {
            LoopLimit = 66_666;
            MemberRenamer = member => member.Name;
            StrictVariables = true;
            
            PushGlobal(customScriptObject);
            PushGlobal(mainScriptObject);
        }


        public void AddRenderedItem(RenderedItem file)
        {
            renderedItems.Add(file);
        }

        public List<RenderedItem> GetRenderedItems()
        {
            return renderedItems;
        }
    }
}