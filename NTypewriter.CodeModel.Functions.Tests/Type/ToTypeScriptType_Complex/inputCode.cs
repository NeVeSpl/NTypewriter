using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Type.ToTypeScriptType_Complex
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0169
    class ToTypeScriptType_Complex
    {
        MyGeneric<int> mygeneric;
        int[] array;
        List<int> list;
        MyGeneric<int?> mygenericofnullable;
        int?[] arrayofnullable;
        List<int?> listofnullable;
        MyGeneric<int?>? genericnull;
        int?[]? arraynull;
        List<int?>? listnull;
        Dictionary<string, short> dict;
    }



    class MyGeneric<T>
    {

    }
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0169
}
