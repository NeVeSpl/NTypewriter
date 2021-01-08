using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Parameter : SymbolBase, IParameter
    {
        private readonly IParameterSymbol symbol;      

        public IType Type => NTypewriter.CodeModel.Roslyn.Type.Create(symbol.Type);
        public object DefaultValue => symbol.HasExplicitDefaultValue ? symbol.Type.GetDefaultConstantValueAsString(symbol.ExplicitDefaultValue) : null;
        public bool HasDefaultValue => symbol.HasExplicitDefaultValue;


        private Parameter(IParameterSymbol symbol) : base(symbol)
        {
            this.symbol = symbol;
        }


        public static IParameter Create(IParameterSymbol symbol)
        {
            return new Parameter(symbol);
        }

        public override string ToString()
        {
            return  $"{Type.Name} {Name}";
        }
    }
}