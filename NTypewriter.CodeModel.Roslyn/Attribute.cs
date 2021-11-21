using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NTypewriter.CodeModel.Roslyn
{
    internal sealed class Attribute : IAttribute
    {
        private readonly AttributeData attributeData;
        private readonly Lazy<AttributeArgumentCollection> arguments;

        public IEnumerable<IAttributeArgument> Arguments => arguments.Value;
        public IClass AttributeClass => Class.Create(attributeData.AttributeClass);
        public string FullName => attributeData.AttributeClass.ToDisplayString();
        public string Name
        { 
            get
            {
                var name = attributeData.AttributeClass.Name;
                if (name.EndsWith("Attribute"))
                {
                    name = name.Substring(0, name.Length - "Attribute".Length);
                }
                return name;
            }
        }


        public Attribute(AttributeData attributeData)
        {            
            this.attributeData = attributeData;
            arguments = new Lazy<AttributeArgumentCollection>(LoadAttributeArguments);
        }


        private AttributeArgumentCollection LoadAttributeArguments()
        {
            var result = AttributeArgumentCollection.Create();

            if (attributeData.AttributeConstructor != null)
            {
                for (int i = 0; i < attributeData.AttributeConstructor.Parameters.Length; i++)
                {
                    var parameter = (IParameterSymbol)attributeData.AttributeConstructor.Parameters[i];
                    var argument = attributeData.ConstructorArguments[i];

                    var attr = new AttributeArgument(argument, true, parameter.Name);
                    result.Add(attr);
                }
            }

            var namedAttributes = attributeData.NamedArguments.Select(x => new AttributeArgument(x.Value, false, x.Key));
            result.AddRange(namedAttributes);

            return result;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}