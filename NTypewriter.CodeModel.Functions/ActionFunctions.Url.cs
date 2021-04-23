using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// Action functions
    /// </summary>
    public static partial class ActionFunctions
    {
        /// <summary>
        /// Returns the url for the Web API action based on route attributes (or the supplied convention route if no attributes are present).
        /// Route parameters are converted to TypeScript string interpolation syntax by prefixing all parameters with $ e.g. ${id}.
        /// Optional parameters are added as QueryString parameters for GET and HEAD requests.
        /// </summary>
        public static string Url(this IMethod method)
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
            route = AppendFromQuery(method, route);

            return route;
        }


        private static string GetRouteFromTypeAttributes(IType type)
        {
            var routeAttribute = type.Attributes.FirstOrDefault(a => a.Name == "RoutePrefix" || a.Name == "Route");
            var route = routeAttribute?.Arguments.Where(x => x.Name == "template" || x.Name == "prefix").Select(x => x.Value.ToString()).FirstOrDefault();

            if (String.IsNullOrEmpty(route) && type.BaseType != null)
            {
                route = GetRouteFromTypeAttributes(type.BaseType);
            }

            return route;
        }
        private static string GetRouteFromMethodAttributes(IMethod method)
        {
            var routeAttributes = method.Attributes.Where(a => a.Name == "Route" || a.Name.StartsWith("Http"));
            var route = routeAttributes.SelectMany(x => x.Arguments).Where(x => x.Name == "template").Select(x => x.Value).Where(x => x != null).FirstOrDefault()?.ToString();
            return route;
        }
        private static string ReplaceSpecialParameters(IMethod method, string route)
        {
            string controllerName = method.ContainingType.BareName;
            controllerName = controllerName.EndsWith("Controller") ? controllerName.Substring(0, controllerName.Length - "Controller".Length) : controllerName;
            route = route.Replace("{controller}", controllerName).Replace("[controller]", controllerName);

            var action = method.Attributes.FirstOrDefault(a => a.Name == "ActionName")?.Arguments.FirstOrDefault(x => x.Name == "name")?.Value.ToString();
            string actionName = action ?? method.BareName;
            route = route.Replace("{action}", actionName).Replace("[action]", actionName);

            return route;
        }

        private static readonly Regex RouteParameterRegex = new Regex(@"\{(\w+).*?\}", RegexOptions.Singleline | RegexOptions.Compiled);
        private static string ConvertRouteParameters(string route)
        {
            return RouteParameterRegex.Replace(route, m => $"${{{m.Groups[1].Value}}}");
        }
        private static string AppendQueryString(IMethod method, string route)
        {
            var parameterAttributeBlackList = new[] { "FromHeader", "FromBody", "FromRoute", "FromServices" };
            var queryParameters = new List<string>();

            foreach (var parameter in method.Parameters.Where(p => p.Type.IsSimple() && p.Attributes.All(x => !parameterAttributeBlackList.Contains(x.Name))))
            {
                if (!route.Contains($"${{{parameter.BareName}}}"))
                {
                    if (parameter.Type.Name == "string")
                    {
                        queryParameters.Add($"{parameter.BareName}=${{encodeURIComponent({parameter.BareName})}}");
                    }
                    else
                    {
                        queryParameters.Add($"{parameter.BareName}=${{{parameter.BareName}}}");
                    }
                    
                }
            }
            if (queryParameters.Any())
            {
                var prefix = route.Contains("?") ? "&" : "?";
                route += $"{prefix}{String.Join("&", queryParameters)}";
            }
            return route;
        }



        private static string AppendFromQuery(IMethod method, string route)
        {
            string connector = route.Contains("?") ? "&" : "?";
            var builder = new StringBuilder();
            foreach (IParameter parameter in method.Parameters)
            {
                if ((!parameter.Type.IsSimple()) && parameter.Attributes.Any(x => x.Name == "FromQuery"))
                {
                    if (parameter.Type is IClass @class)
                    {                   
                        foreach (var prop in @class.Properties)
                        {
                            builder.Append(connector);
                            if (parameter.Type.Name == "string")
                            {
                                builder.Append($"{prop.BareName.ToLowerFirst()}=${{encodeURIComponent({parameter.BareName}.{prop.BareName.ToLowerFirst()})}}");
                            }
                            else
                            {
                                builder.Append($"{prop.BareName.ToLowerFirst()}=${{{parameter.BareName}.{prop.BareName.ToLowerFirst()}}}");
                            }
                            connector = "&";
                        }
                    }
                }
            }
            var postfix = builder.ToString();
            return route + postfix;
        }
    }
}