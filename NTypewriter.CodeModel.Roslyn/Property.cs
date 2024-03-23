using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Property : SymbolBase, IProperty
    {
        private readonly IPropertySymbol symbol;      

        public IType Type => NTypewriter.CodeModel.Roslyn.Type.Create(symbol.Type, this);
        public bool IsIndexer => symbol.IsIndexer;
        public bool IsWriteOnly => symbol.IsWriteOnly;
        public bool IsReadOnly => symbol.IsReadOnly;
        public bool IsSealed => symbol.IsSealed;

        public IMethod GetMethod => symbol.GetMethod != null ? Method.Create(symbol.GetMethod) : null;
        public IMethod SetMethod => symbol.SetMethod != null ? Method.Create(symbol.SetMethod) : null;


        private Property(IPropertySymbol symbol) : base (symbol)
        {
            this.symbol = symbol;            
        }


        public static IProperty Create(IPropertySymbol symbol)
        {
            return new Property(symbol);
        }
    }
}