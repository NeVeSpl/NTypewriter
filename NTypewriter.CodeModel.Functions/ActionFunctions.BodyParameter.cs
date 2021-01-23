using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns parameter that receives content sent to a webapi action in a request body.
        /// </summary>
        public static IParameter BodyParameter(this IMethod method)
        {
            var parameterTypeBlackList = new[] { "CancellationToken" };
            var parameterAttributeBlackList = new[] { "FromHeader", "FromQuery", "FromRoute", "FromServices" };

           
            var dataParameters = method.Parameters
                .Where(x => !parameterTypeBlackList.Contains(x.Type.Name))
                .Where(x => !x.Type.IsPrimitive || x.Attributes.Any(y => y.Name == "FromBody"))
                .Where(x => x.Attributes.All(y => !parameterAttributeBlackList.Contains(y.Name)))
                .ToList();

            if (dataParameters.Any())
            {
                return dataParameters.First();
            }

            return null;
        }
    }
}