using SenpaiMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SenpaiMarketplace
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            using (SenpaiDatabase database = new SenpaiDatabase())
            {
                database.Database.CreateIfNotExists();
            }

            AreaRegistration.RegisterAllAreas();

            JsonMediaTypeFormatter json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
