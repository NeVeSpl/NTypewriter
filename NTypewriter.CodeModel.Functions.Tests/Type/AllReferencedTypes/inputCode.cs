using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Type.ToTypeScriptDefault
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0169
    class AllReferencedTypes : BaseType
    {
        FieldType field;
        int intField;
        string stringField;
        EnumType enumField;
        IEnumerable<EnumerableType> enumerableField;
        GenericType<int> genericField;
        long? longType;
        Func<string> funcType;
        ArrayType[] arrayField;
        PropertyType property { get; set; }

        MethodReturnType Method(ParameterType type)
        {
            return null;
        }

        Task<MethodReturnType2> Method2(ParameterType type)
        {
            return null;
        }



    }

    class BaseType
    {

    }

    class FieldType
    {

    }

    class PropertyType
    {

    }

    class MethodReturnType
    {

    }

    class MethodReturnType2
    {

    }

    class ParameterType
    {

    }

    enum EnumType
    {
        First,
        Second
    }

    class EnumerableType
    {

    }

    class GenericType<T>
    {

    }
    class ArrayType
    {

    }



#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0169
}

