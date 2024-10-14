using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    internal static class ITypeExtensions
    {
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0#simple-types
        public static bool IsSimple(this IType type)
        {
            if (type.IsNullable)
            {
                var nonNullableType = GetUnderlyingNonNullableType(type);
                if (nonNullableType != null)
                    return IsSimple(nonNullableType);
            }
            switch (type.FullName)
            {
                case "System.DateTime":
                case "System.DateTimeOffset":
                case "System.Guid":
                case "System.Decimal":
                case "System.TimeSpan":
                case "System.Uri":
                case "System.Version":
                    return true;
            }

            return type.IsPrimitive || type.IsEnum;
        }

        public static IType GetUnderlyingNonNullableType(this IType type)
        {
            if (!type.IsNullable)
                return null;
            if (type.IsReferenceType)
                return type.OriginalDefinition;
            return type.TypeArguments.FirstOrDefault();
        }
    }
}
