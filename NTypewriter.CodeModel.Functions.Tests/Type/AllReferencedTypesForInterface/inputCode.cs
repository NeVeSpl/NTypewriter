using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.CodeModel.Functions.Tests.Type.AllReferencedTypesForInterface
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0169
    interface AllReferencedTypesForInterface : IInterfaceType, IGenericInterface<GenericInterfaceArgumentType>
    {

        PropertyType property { get; set; }

        event Action<EventTypeArgument> Event;

        MethodReturnType Method(ParameterType type);

        public IndexerReturnType this[int i]
        {
            get;
        }
    }
    interface IInterfaceType
    {

    }

    interface IGenericInterface<TypeOnGenericInterfaceType>
    {

    }

    class GenericInterfaceArgumentType
    {

    }

    class PropertyType
    {

    }

    class EventTypeArgument
    {

    }

    class MethodReturnType
    {

    }

    class IndexerReturnType
    {

    }



    class ParameterType
    {

    }

   




#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0169
}

