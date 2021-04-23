using System.Collections.Generic;
using System.Linq;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns parameters that receive content sent to a webapi action.
        /// If _withoutBodyParameter_ is specified as true, then the Parameter list returned will not include the parameter that is being sent in the body of the request.
        /// </summary>
        public static IEnumerable<IParameter> Parameters(this IMethod method, bool withoutBodyParameter = false)
        {
            var parameterTypeBlackList = new[] { "CancellationToken" };
            var parameterAttributeBlackList = new[] { "FromServices" };

           
            var dataParameters = method.Parameters
                .Where(x => !parameterTypeBlackList.Contains(x.Type.Name))                
                .Where(x => x.Attributes.All(y => !parameterAttributeBlackList.Contains(y.Name)))
                .ToList();           

            if (withoutBodyParameter)
            {
                var bodyParameter = method.BodyParameter();
                if (bodyParameter != null)
                {
                    return dataParameters.Where(p => p.Name != bodyParameter.Name).ToList();
                }
            }

            return dataParameters.ToList();
        }
    }
}