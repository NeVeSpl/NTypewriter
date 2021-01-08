using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class EnumValue : IEnumValue
    {
        private readonly IFieldSymbol symbol;

        public object Value => symbol.ConstantValue;
        public string Name => symbol.Name;


        private EnumValue(IFieldSymbol symbol)
        {
            this.symbol = symbol;
        }       


        public static IEnumValue Create(IFieldSymbol symbol)
        {           
            return new EnumValue(symbol);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}