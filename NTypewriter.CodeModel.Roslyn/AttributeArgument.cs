using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class AttributeArgument : IAttributeArgument
    {
        private readonly Microsoft.CodeAnalysis.TypedConstant typedConstant;    

        public bool IsFromConstructor { get; private set; }
        public string Name { get; private set; }
        public IType Type => NTypewriter.CodeModel.Roslyn.Type.Create(typedConstant.Type);
        public object Value => TypedConstant.Create(typedConstant);

        public AttributeArgument(Microsoft.CodeAnalysis.TypedConstant typedConstant, bool isFromConstructor, string name)
        {
            this.typedConstant = typedConstant;
            this.IsFromConstructor = isFromConstructor;
            this.Name = name;          
        }


        public override string ToString()
        {
            string valueAsString = Value?.ToString();
            
            return $"{Name} : {valueAsString}";
        }
    }
}