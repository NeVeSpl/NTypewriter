using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns parameters that receive content sent to a webapi action.
        /// </summary>
        public static IEnumerable<IParameter> Parameters(this IMethod method)
        {
            var parameterTypeBlackList = new[] { "CancellationToken" };
            var parameterAttributeBlackList = new[] { "FromServices" };

           
            var dataParameters = method.Parameters
                .Where(x => !parameterTypeBlackList.Contains(x.Type.Name))                
                .Where(x => x.Attributes.All(y => !parameterAttributeBlackList.Contains(y.Name)))
                .ToList();           

            return dataParameters.ToList();
        }
    }
}