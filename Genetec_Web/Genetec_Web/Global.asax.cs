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
            //types.Add(new Models.Item.Type(types, "Bag", true, false));
            //Models.Item.Type type = types.types.Find(x => x.ID == 11);
            //type.Name = "Bags";
            //type.BulkQuantity = false;
            //type.CanRent = true;
            //types.Modify(type);
            //types.Delete(10);
            //types.types[1].parameters.Delete(types.parameters.parameters.Find(x => x.ID == 3));
            //types.types[0].parameters.Add(types.parameters.parameters.Find(x => x.ID == 4));
            //Models.Item.Type type = new Models.Item.Type("Desktop", false, true, 2);
            //type.parameters.Add(param.parameters.ElementAt(2));
        }
    }
}
