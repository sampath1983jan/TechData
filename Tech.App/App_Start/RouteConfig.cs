using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
 


namespace Tech.App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



           

            //routes.Add("DomainRoute", new DomainRoute(
            //    "{controller}.example.com",                             // Domain with parameters
            //    "{action}/{id}",                                        // URL with parameters
            //    new { controller = "Authentication", action = "Login", id = "" }  // Parameter defaults
            //));

            //routes.Add("DomainRoute", new DomainRoute(
            //    "{controller}-{action}.example.com",                             // Domain with parameters
            //    "{id}",                                        // URL with parameters
            //    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            //));



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
    public class DomainData
    {
        public string Protocol { get; set; }
        public string HostName { get; set; }
        public string Fragment { get; set; }
    }
    public static class LinkExtensions
    {
        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary(), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues, htmlAttributes, requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), requireAbsoluteUrl);
        }

        //public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        //{
        //    if (requireAbsoluteUrl)
        //    {
        //        HttpContextBase currentContext = new HttpContextWrapper(System.Web.HttpContext.Current);
        //        RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

        //        routeData.Values["controller"] = controllerName;
        //        routeData.Values["action"] = actionName;

        //        DomainRoute domainRoute = routeData.Route as DomainRoute;
        //        if (domainRoute != null)
        //        {
        //            DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
        //      //      return htmlHelper.ActionLink(linkText, actionName, controllerName, domainData.Protocol, domainData.HostName, domainData.Fragment, routeData.Values, null);
        //        }
        //    }
        // //   return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        //}
    }
    public class DomainRoute : Route
    {
        private Regex domainRegex;
        private Regex pathRegex;

        public string Domain { get; set; }

        public DomainRoute(string domain, string url, RouteValueDictionary defaults)
            : base(url, defaults, new MvcRouteHandler())
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            Domain = domain;
        }

        public DomainRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
            : base(url, new RouteValueDictionary(defaults), routeHandler)
        {
            Domain = domain;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            // Build regex
            domainRegex = CreateRegex(Domain);
            pathRegex = CreateRegex(Url);

            // Request information
            string requestDomain = httpContext.Request.Headers["host"];
            if (!string.IsNullOrEmpty(requestDomain))
            {
                if (requestDomain.IndexOf(":") > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
                }
            }
            else
            {
                requestDomain = httpContext.Request.Url.Host;
            }
            string requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) +
                                 httpContext.Request.PathInfo;

            // Match domain and route
            Match domainMatch = domainRegex.Match(requestDomain);
            Match pathMatch = pathRegex.Match(requestPath);

            // Route data
            RouteData data = null;
            if (domainMatch.Success && pathMatch.Success && requestDomain.ToLower() != "tg.local" &&
                requestDomain.ToLower() != "tg.terrasynq.net" && requestDomain.ToLower() != "www.townsgossip.com" &&
                requestDomain.ToLower() != "townsgossip.com")
            {
                data = new RouteData(this, RouteHandler);

                // Add defaults first
                if (Defaults != null)
                {
                    foreach (KeyValuePair<string, object> item in Defaults)
                    {
                        data.Values[item.Key] = item.Value;
                    }
                }

                // Iterate matching domain groups
                for (int i = 1; i < domainMatch.Groups.Count; i++)
                {
                    Group group = domainMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = domainRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                        {
                            if (!string.IsNullOrEmpty(group.Value))
                            {
                                data.Values[key] = group.Value;
                            }
                        }
                    }
                }

                // Iterate matching path groups
                for (int i = 1; i < pathMatch.Groups.Count; i++)
                {
                    Group group = pathMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = pathRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                        {
                            if (!string.IsNullOrEmpty(group.Value))
                            {
                                data.Values[key] = group.Value;
                            }
                        }
                    }
                }
            }

            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, RemoveDomainTokens(values));
        }

        public DomainData GetDomainData(RequestContext requestContext, RouteValueDictionary values)
        {
            // Build hostname
            string hostname = Domain;
            foreach (KeyValuePair<string, object> pair in values)
            {
                hostname = hostname.Replace("{" + pair.Key + "}", pair.Value.ToString());
            }

            // Return domain data
            return new DomainData
            {
                Protocol = "http",
                HostName = hostname,
                Fragment = ""
            };
        }

        private Regex CreateRegex(string source)
        {
            // Perform replacements
            source = source.Replace("/", @"\/?");
            source = source.Replace(".", @"\.?");
            source = source.Replace("-", @"\-?");
            source = source.Replace("{", @"(?<");
            source = source.Replace("}", @">([a-zA-Z0-9_\-]*))");

            return new Regex("^" + source + "$");
        }

        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
        {
            var tokenRegex =
                new Regex(
                    @"({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?({[a-zA-Z0-9_\-]*})*\.?\/?");
            Match tokenMatch = tokenRegex.Match(Domain);
            for (int i = 0; i < tokenMatch.Groups.Count; i++)
            {
                Group group = tokenMatch.Groups[i];
                if (group.Success)
                {
                    string key = group.Value.Replace("{", "").Replace("}", "");
                    if (values.ContainsKey(key))
                        values.Remove(key);
                }
            }

            return values;
        }
    }

}


