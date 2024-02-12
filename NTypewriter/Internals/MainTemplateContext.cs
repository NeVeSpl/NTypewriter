using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTypewriter.Ports;
using Scriban;
using Scriban.Syntax;

namespace NTypewriter.Internals
{
    internal sealed class MainTemplateContext : TemplateContext
    {        
        private readonly List<RenderedItem> renderedItems = new List<RenderedItem>();
        private readonly IExternalOutput externalOutput;
        private readonly IExpressionCompiler expressionCompiler;


        public MainTemplateContext(DataScriptObject dataScriptObject, CustomFunctionsScriptObject customScriptObject, IExternalOutput externalOutput, IExpressionCompiler expressionCompiler) : base(BuiltinFunctionsScriptObject.Singleton)
        {
            LoopLimit = 66_666;
            MemberRenamer = member => member.Name;
            StrictVariables = true;
            EnableRelaxedMemberAccess = false;

            PushGlobal(customScriptObject);
            PushGlobal(dataScriptObject);
            this.externalOutput = externalOutput;
            this.expressionCompiler = expressionCompiler;

            TryGetMember = TryGetMemberImp;
        }


        private bool TryGetMemberImp(TemplateContext context, Scriban.Parsing.SourceSpan span, object target, string member, out object value)
        {
            if (target is IEnumerable<object> symbold)
            {
               
            }

            value = null;
            return false;
        }


        public void AddRenderedItem(RenderedItem file)
        {
            renderedItems.Add(file);
        }
        public void WriteOnExternalOutput(string text)
        {
            externalOutput?.Write(text);
        }
        public Func<object, bool> CompilePredicate(string predicate, Type type)
        {
            if (expressionCompiler == null)
            {
                throw new Exception("Expression compiler is unavailable");
            }
            return expressionCompiler.CompilePredicate(predicate, type);
        }


        public List<RenderedItem> GetRenderedItems()
        {
            return renderedItems;
        }


        public override object Evaluate(ScriptNode scriptNode, bool aliasReturnedFunction)
        {
            return base.Evaluate(scriptNode, aliasReturnedFunction);
        }
        public override ValueTask<object> EvaluateAsync(ScriptNode scriptNode, bool aliasReturnedFunction)
        {
            return base.EvaluateAsync(scriptNode, aliasReturnedFunction);
        }       
    }
}