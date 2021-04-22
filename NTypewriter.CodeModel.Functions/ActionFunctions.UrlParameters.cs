using System.Collections.Generic;
using System.Linq;

namespace NTypewriter.CodeModel.Functions
{
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns parameters of the webapi action that are required to produce the Url for the action
        /// </summary>
        public static IEnumerable<IParameter> UrlParameters(this IMethod method)
        {
            var parameterList = method.Parameters();
            var bodyParameter = method.BodyParameter();

            return bodyParameter != null
                ? parameterList.Where(p => p.Name != bodyParameter.Name).ToList()
                : parameterList;
        }
    }
}