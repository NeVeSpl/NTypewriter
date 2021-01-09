using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Method functions
    /// </summary>
    public static class MethodFunctions
    {
        private static readonly string[] httpVerbs = { "get", "post", "put", "delete", "patch", "head", "options", "connect", "trace" };


        /// <summary>
        /// Returns the http method used with a webapi action.
        /// The http method is extracted from Http* or AcceptVerbs attribute or by naming convention if no attributes are specified.
        /// </summary>
        public static string ActionHttpMethod(this IMethod method)
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


        /// <summary>
        /// Returns type name of the parameter that is sent to a webapi action in a request body.
        /// </summary>
        public static string ActionTypeSentByBody(this IMethod method)
        {
            var parameterTypeBlackList = new[] { "CancellationToken" };
            var parameterAttributeBlackList = new[] { "FromHeader", "FromQuery", "FromRoute" , "FromServices" };

            // CancellationToken will never be send from TypeScript, filter them out before generating RequestData
            var dataParameters = method.Parameters
                .Where(x => !parameterTypeBlackList.Contains(x.Type.Name))
                .Where(x => !x.Type.IsPrimitive || x.Attributes.Any(y => y.Name == "FromBody"))
                .Where(x => x.Attributes.All(y => !parameterAttributeBlackList.Contains(y.Name)))
                .ToList();

            if (dataParameters.Any())
            {
                return dataParameters.First().Type.Name;
            }

            return "";
        }


        /// <summary>
        /// Returns the url for the Web API action based on route attributes (or the supplied convention route if no attributes are present).
        /// Route parameters are converted to TypeScript string interpolation syntax by prefixing all parameters with $ e.g. ${id}.
        /// Optional parameters are added as QueryString parameters for GET and HEAD requests.
        /// </summary>
        public static string ActionUrl(this IMethod method)
        {
            var prefix = GetRouteFromTypeAttributes(method.ContainingType);
            var postfix = GetRouteFromMethodAttributes(method);

            string route = null;

            if (postfix?.StartsWith("~/") == true)
            {
                route = postfix.Substring(2);
            }
            if (postfix?.StartsWith("/") == true)
            {
                route = postfix.Substring(1);
            }
            if (route == null)
            {
                route = $"{prefix?.Trim('/')}/{postfix}".Trim('/');
            }

            route = ReplaceSpecialParameters(method, route);
            route = ConvertRouteParameters(route);
            route = AppendQueryString(method, route);

            return route;
        }


        private static string GetRouteFromTypeAttributes(IType type)
        {
            var routeAttribute = type.Attributes.FirstOrDefault(a => a.Name == "RoutePrefix" || a.Name == "Route");
            var route = routeAttribute?.Arguments.Where(x => x.Name == "template").Select(x => x.Value.ToString()).FirstOrDefault();

            if (String.IsNullOrEmpty(route) && type.BaseType != null)
            {
                route = GetRouteFromTypeAttributes(type.BaseType);
            }

            return route;
        }
        private static string GetRouteFromMethodAttributes(IMethod method)
        {
            var routeAttribute = method.Attributes.FirstOrDefault(a => a.Name == "Route" || a.Name.StartsWith("Http"));
            var route = routeAttribute?.Arguments.Where(x => x.Name == "template").FirstOrDefault()?.Value.ToString();
            return route;
        }
        private static string ReplaceSpecialParameters(IMethod method, string route)
        {
            string controllerName =  method.ContainingType.BareName;
            controllerName = controllerName.EndsWith("Controller") ? controllerName.Substring(0, controllerName.Length -"Controller".Length) : controllerName;
            route = route.Replace("{controller}", controllerName).Replace("[controller]", controllerName);

            var action = method.Attributes.FirstOrDefault(a => a.Name == "ActionName")?.Arguments.FirstOrDefault(x => x.Name == "name")?.Value.ToString();
            string actionName = action ?? method.BareName;
            route = route.Replace("{action}", actionName).Replace("[action]", actionName);            

            return route;
        }

        private static readonly Regex RouteParameterRegex = new Regex(@"\{(\w+).*?\}", RegexOptions.Singleline | RegexOptions.Compiled);
        private static string ConvertRouteParameters(string route)
        {
            return RouteParameterRegex.Replace(route,  m => $"${{{m.Groups[1].Value}}}");
        }
        private static string AppendQueryString(IMethod method, string route)
        {
            var parameterAttributeBlackList = new[] { "FromHeader", "FromBody", "FromRoute", "FromServices" };

            var prefix = route.Contains("?") ? "&" : "?";
            var queryParameters = new List<string>();

            foreach (var parameter in method.Parameters.Where(p => p.Type.IsPrimitive && p.Attributes.All(x => !parameterAttributeBlackList.Contains(x.Name))))
            {
                if (!route.Contains($"${{{parameter.BareName}}}"))
                {
                    queryParameters.Add($"{parameter.BareName}=${{{parameter.BareName}}}");
                }
            }
            if (queryParameters.Any())
            {
                route += $"{prefix}{String.Join("&", queryParameters)}";
            }
            return route;
        }
    }
}