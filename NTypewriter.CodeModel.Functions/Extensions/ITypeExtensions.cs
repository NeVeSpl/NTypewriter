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
                if (type.TypeArguments.Any())
                {
                    return IsSimple(type.TypeArguments.First());
                }
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
    }
}
