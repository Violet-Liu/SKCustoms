using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SKCustoms.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var allowedOrigin = ConfigurationManager.AppSettings["cors:allowedOrigin"] ?? "localhost:8080";
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true });
            //GlobalConfiguration.Configuration.EnableCors(new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true });

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new JsonCallbackAttribute());
            config.Filters.Add(new LogExceptionAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
