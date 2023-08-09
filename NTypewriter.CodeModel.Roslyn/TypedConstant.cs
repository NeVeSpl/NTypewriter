using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class TypedConstant : ITypedConstant
    {
        private readonly Microsoft.CodeAnalysis.TypedConstant typedConstant;

        public IType Type => NTypewriter.CodeModel.Roslyn.Type.Create(typedConstant.Type);
        public object Value => typedConstant.Value;


        private TypedConstant(Microsoft.CodeAnalysis.TypedConstant typedConstant)
        {
            this.typedConstant = typedConstant;
        }



        public static object Create(Microsoft.CodeAnalysis.TypedConstant typedConstant)
        {
            if (typedConstant.Kind == TypedConstantKind.Array)
            {
                return typedConstant.Values.Select(x => Create(x)).ToArray();
            }

            if (typedConstant.Kind == TypedConstantKind.Enum)
            {
                return new TypedConstant(typedConstant);
            }

            if (typedConstant.Value is INamedTypeSymbol namedTypeSymbol)
            {
                return NamedType.Create(namedTypeSymbol);
            }

            return typedConstant.Value;
        }

        public override string ToString()
        {
            string valueAsString = typedConstant.Type.GetDefaultConstantValueAsString(typedConstant.Value)?.ToString();

            return valueAsString;
        }
    }
}
