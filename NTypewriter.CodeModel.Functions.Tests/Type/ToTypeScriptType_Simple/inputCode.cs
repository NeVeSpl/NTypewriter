using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Type.ToTypeScriptType_Simple
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0169
    class ToTypeScriptType_Simple
    {
       
        bool boolType;
        //void voidType;
        int integer;    
        int? nullableInteger;
        string str;
        string? nullableStr;
        MyEnum enumik;
        MyEnum? nullableEnumik;
        Task<int> task;
        [Required]
        int? nullableInteger2;
        dynamic dynamic;
        TimeSpan timeSpan;
        TimeSpan? optionalTimeSpan;
        char character;
        private char? nullableCharacter;



    }


    

    enum MyEnum
    {

    }
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0169
}
