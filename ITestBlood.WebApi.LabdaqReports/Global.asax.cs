using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ITestBlood.WebApi.LabdaqReports
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var webapi_config = new WebApiConfig();
            System.Web.Routing.RouteTable.Routes.RouteExistingFiles = true;
            GlobalConfiguration.Configure(webapi_config.Register);
        }
    }

}
