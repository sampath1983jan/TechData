using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

 
    public class WebApiConfig
    {
    public static void Register(HttpConfiguration configuration)
    {
        configuration.MapHttpAttributeRoutes();

        configuration.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        formatter.SerializerSettings.ContractResolver =
            new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
    }

}

 