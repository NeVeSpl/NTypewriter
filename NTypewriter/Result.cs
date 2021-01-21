using System.Collections.Generic;
using System.Linq;
using Scriban;
using Scriban.Syntax;

namespace NTypewriter
{
    public class Result
    {
        private readonly List<MessageItem> messages = new List<MessageItem>();
        private readonly List<RenderedItem> renderedItems = new List<RenderedItem>();
        private bool hasErrors;
       

        public bool HasErrors
        {
            get
            {
                return hasErrors;
            }
        }
        public IReadOnlyList<MessageItem> Messages
        {
            get
            {
                return messages;
            }
        }
        public IReadOnlyList<RenderedItem> Items
        {
            get
            {
                return renderedItems;
            }
        }


        internal void AddMsgFromScribanTemplateParsing(LogMessageBag bag)
        {
            hasErrors |= bag.HasErrors;
            messages.AddRange(bag.Select(x => new MessageItem(x)));
        }

        internal void AddMsgFromScribanException(ScriptRuntimeException exception)
        {
            hasErrors |= true;
            messages.Add(new MessageItem(exception));
        }

        internal void AddRenderedItems(List<RenderedItem> itemsToAdd)
        {
            renderedItems.AddRange(itemsToAdd);
        }
    }
}