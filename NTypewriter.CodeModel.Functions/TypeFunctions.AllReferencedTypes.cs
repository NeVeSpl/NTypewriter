using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class TypeFunctions
    {
        public static IEnumerable<IType> AllReferencedTypes(this IType type)
        {
            var foundTypes = new HashSet<IType>(new TypeComparer());

            if (type is IClass @class)
            {
                foreach (var property in @class.Properties)
                {
                    InspectType(foundTypes, property.Type);
                }
                foreach (var method in @class.Methods)
                {
                    InspectType(foundTypes, method.ReturnType);
                    foreach (var parameter in method.Parameters)
                    {
                        if (IsNotFromServices(parameter))
                        {
                            InspectType(foundTypes, parameter.Type);
                        }
                    }
                }
                foreach (var field in @class.Fields)
                {
                    InspectType(foundTypes, field.Type);
                }
                if (@class.HasBaseClass)
                {
                    InspectType(foundTypes, @class.BaseClass);
                }
            }

            if (foundTypes.Contains(type))
            {
                foundTypes.Remove(type);
            }

            return foundTypes.ToList();
        }

        private static void InspectType(HashSet<IType> foundTypes, IType type)
        {
            if (foundTypes.Contains(type))
            {
                return;
            }
            if (type.IsPrimitive)
            {
                return;
            }
            if (type.IsGeneric)
            {
                foreach (var typeArgument in type.TypeArguments)
                {
                    InspectType(foundTypes, typeArgument);
                }
            }
            if (type.IsNullable)
            {
                return;
            }
            if (type.Namespace.StartsWith("System.") || type.Namespace.StartsWith("Microsoft.") ||
                type.Namespace.Equals("System") || type.Namespace.Equals("Microsoft")
                )
            {
                return;
            }

            foundTypes.Add(type);
        }

        static bool IsNotFromServices(IParameter p)
        {
            if (p.Attributes.Any(x => x.Name == "FromServices"))
            {
                return false;
            }
            return true;
        }
    }

    class TypeComparer : IEqualityComparer<IType>
    {
        public bool Equals(IType x, IType y)
        {
            return x.FullName.Equals(y.FullName);
        }

        public int GetHashCode(IType obj)
        {
            return obj.FullName.GetHashCode();
        }
    }
}
