using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Action functions
    /// </summary>
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns type that is returned from action unwrapped from Task and ActionResult generics
        /// </summary>
        public static IType ReturnType(this IMethod method)
        {
            var result = method.ReturnType;

            var responseTypeAttribute = method.Attributes.FirstOrDefault(a => a.Name == "ResponseType" || a.Name == "ProducesResponseType");
            var responseTypeArgument = responseTypeAttribute?.Arguments.FirstOrDefault(x => x.Name == "responseType" || x.Name == "Type");
            if (responseTypeArgument != null)
            {
                return responseTypeArgument.Value as IType;
            }
            
            while (result != null && (result.BareName == "Task" || result.BareName == "ActionResult"))
            {
                if (result.IsGeneric)
                {
                    result = result.TypeArguments.FirstOrDefault();
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
    }
}