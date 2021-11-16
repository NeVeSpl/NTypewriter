using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class TypeFunctions
    {
        /// <summary>
        /// The default value of the type.
        /// (Dictionary types returns {}, enumerable types returns [],
        /// boolean types returns false, numeric types returns 0, void returns void(0),
        /// Guid types return empty guid string, Date types return new Date(0),
        /// all other types return null)
        /// </summary>
        public static string ToTypeScriptDefault(this IType type)
        {
            if (type.IsNullable)
            {
                if (!((type is ITypeReferencedByMember typeReference) && (typeReference.Parent?.Attributes.Any(x => x.Name == "Required")) == true))
                {
                    return "null";
                }
                else
                {
                    type = type.TypeArguments.FirstOrDefault() ?? type;
                }
            }
            if (type.IsEnum)
            {
                return "0";
            }
            switch (type.FullName)
            {
                case "System.Boolean":
                    return "false";
                case "System.String":
                    return "\"\"";
                case "System.Guid":
                    return "\"00000000-0000-0000-0000-000000000000\"";
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
                    return "0";
                case "System.Void":
                    return "void(0)";
            }
            if (type.IsEnumerable)
            {
                return "[]";
            }

            return "{}";
        }
    }
}
