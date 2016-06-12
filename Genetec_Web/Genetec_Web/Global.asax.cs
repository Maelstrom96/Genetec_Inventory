using Genetec_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Genetec_Web.Models.Item;

namespace Genetec_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Types types = new Types();
            //Models.Item.Type type = new Models.Item.Type("Desktop", false, true, 2);
            //type.parameters.Add(param.parameters.ElementAt(2));
        }
    }
}
