using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ITestBlood.WebApi.LabdaqReports
{
    public class WebApiConfig
    {
        public void Register(HttpConfiguration config)
        {
            //Json formatter config
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Formatting.Indented;

            // Jil is much faster than Json.net
            //var json_formatter = new JilFormatter();

            var json_formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            };

            config.Services.Replace(
                typeof(IContentNegotiator),
                new JsonContentNegotiator(json_formatter)
                );

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new CancelledTaskBugWorkaroundMessageHandler());

            MapRoutes(config);
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            );
        }
    }
}
