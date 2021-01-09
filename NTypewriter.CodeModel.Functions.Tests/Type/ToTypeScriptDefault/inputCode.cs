using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Type.ToTypeScriptDefault
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0169
    class ToTypeScriptDefault
    {

        bool boolType;

        bool? boolNull;
        int integer;

        int? nullableInteger;

        string str;
        string? nullableStr;
        MyEnum enumik;
        MyEnum? nullableEnumik;
        List<int> list;
        MyGeneric<int> myGen;




    }


    class MyGeneric<T>
    {

    }

    enum MyEnum
    {

    }
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0169
}

