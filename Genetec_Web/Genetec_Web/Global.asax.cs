using Genetec_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Genetec_Web.Models.Item;
using Genetec_Web.Models.Active_Directory;

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

            List<ADUser> users = ADUsers.GetADUsers();

            Types types = new Types();
            Genetec_Web.Models.Item.Models models = new Genetec_Web.Models.Item.Models(ref types);

            Models.Item.Type type = types.List.Find(x => x.ID == 2);
            Model model = new Model("M4400", "", ref type);
            //models.Add(model);
            model.ID = 2;
            model.Value = "M4400-1";
            model.Description = "DELL";
            model.type = types.List.Find(x => x.ID == 1);

            models.Modify(model);

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
