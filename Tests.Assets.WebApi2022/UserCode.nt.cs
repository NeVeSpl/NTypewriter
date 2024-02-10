using System;
using System.Linq;
using System.Collections.Generic;
using NTypewriter.CodeModel;
using NTypewriter.CodeModel.Functions;

namespace _
{
    class NTypewriterCustom
    {
        public static string MapTypeName(IType type)
        {
            List<IType> typeArguments = type.TypeArguments.ToList();
            switch (typeArguments.Count)
            {
                case 2:
                    if (type.Name == $"KeyValuePair<{typeArguments[0]}, {typeArguments[1]}>")
                    {
                        return $"{{key: {typeArguments[0]}, value: {typeArguments[1]}}}";
                    }
                    break;
                case 1:
                    if (type.IsEnumerable)
                    {
                        return $"{MapTypeName(typeArguments[0])}[]";
                    }
                    break;
            }
            return type.ToTypeScriptType();
        }
    }
}