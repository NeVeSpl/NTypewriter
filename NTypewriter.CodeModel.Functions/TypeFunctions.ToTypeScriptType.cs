using System;
using System.Linq;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Type functions
    /// </summary>
    public static partial class TypeFunctions
    {
        /// <summary>
        /// Converts type name to typescript type name
        /// </summary>
        public static string ToTypeScriptType(this IType type, string nullableType = "null")
        {
            if (type == null)
            {
                return null;
            }
            var postfix = String.Empty;
            if (type.IsNullable && nullableType != String.Empty)
            {
                if (!((type is ITypeReferencedByMember typeReference) && (typeReference.Parent?.Attributes.Any(x => x.Name == "Required") == true)))
                {
                    postfix = " | " + (nullableType ?? "null");
                }
            }
            return ToTypeScriptTypePhase2(type, nullableType) + postfix;
        }

        private static string ToTypeScriptTypePhase2(IType type, string nullableType)
        {
            if (type.IsArray)
            {
                // Array : int[]

                if (type.ArrayType.IsNullable && nullableType != String.Empty)
                {
                    return $"({ToTypeScriptType(type.ArrayType, nullableType)})[]";
                }

                return ToTypeScriptType(type.ArrayType, nullableType) + "[]";
            }
            if (type.IsGeneric)
            {
                var arguments = type.TypeArguments.Select(x => ToTypeScriptType(x, nullableType)).ToList();

                if (type.IsNullable && type.IsValueType)
                {
                    // nullable value type : int?
                    return arguments.First();
                }

                if (type.IsEnumerable)
                {
                    if (arguments.Count() == 1)
                    {
                        // List : List<int>

                        var firstArgument = arguments.First();

                        if (firstArgument.Contains("|"))
                        {
                            return $"({firstArgument})[]";
                        }

                        return firstArgument + "[]";
                    }
                    if (arguments.Count() == 2)
                    {
                        // Dictionary
                        return $"{{ [key: {arguments[0]}]: {arguments[1]} }}";
                    }
                }

                // common generic : MyGeneric<int,string>
                var name = TranslateNameToTypeScriptName(type);
                return $"{name}<{ String.Join(",", arguments) }>";
            }

            return TranslateNameToTypeScriptName(type);
        }
        private static string TranslateNameToTypeScriptName(IType type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                    return "boolean";
                case "System.String":
                case "System.Guid":
                    return "string";
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return "number";
                case "System.DateTime":
                case "System.DateTimeOffset":
                    return "Date";
                case "System.Void":
                    return "void";
                case "System.Object":
                case "dynamic":
                    return "any";

            }
            if ((type.Namespace == "System.Threading.Tasks") && (type.BareName == "Task"))
            {
                return "Promise";
            }

            return type.BareName;
        }
    }
}
