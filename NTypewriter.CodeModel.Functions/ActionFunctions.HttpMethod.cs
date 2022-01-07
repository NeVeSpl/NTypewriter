using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Set of functions that operates on IMethod
    /// </summary>
    public static partial class ActionFunctions
    {
        private static readonly string[] httpVerbs = { "get", "post", "put", "delete", "patch", "head", "options", "connect", "trace" };

        /// <summary>
        /// Returns the http method used with a webapi action.
        /// The http method is extracted from Http* or AcceptVerbs attribute or by naming convention if no attributes are specified.
        /// </summary>
        public static string HttpMethod(this IMethod method)
        {
            // HTTP method can be specified with an attribute: AcceptVerbs, HttpDelete, HttpGet, HttpHead, HttpOptions, HttpPatch, HttpPost, or HttpPut.
            // Otherwise, if the name of the controller method starts with one of the httpVerbs, then by convention the action supports that HTTP method. 
            // If none of the above, the method supports POST.

            var httpAttributes = method.Attributes.Where(a => a.Name.StartsWith("Http"));
            var acceptAttribute = method.Attributes.FirstOrDefault(a => a.Name == "AcceptVerbs");

            var verbs = httpAttributes.Select(a => a.Name.Remove(0, "Http".Length).ToLowerInvariant()).ToList();
            if (acceptAttribute != null)
            {
                var args = acceptAttribute.Arguments.First().Value as object[];
                verbs.AddRange(args.Select(x => x.ToString().ToLowerInvariant()));
            }

            // Prefer POST if multiple verbs are specified
            if (verbs.Contains("post"))
            {
                return "post";
            }

            if (verbs.Any())
            {
                return verbs.First();
            }

            var methodName = method.Name.ToLowerInvariant();
            return httpVerbs.FirstOrDefault(v => methodName.StartsWith(v)) ?? "post";
        }
    }
}
