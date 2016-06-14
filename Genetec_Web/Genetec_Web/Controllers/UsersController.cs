using Genetec_Web.Models.Active_Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genetec_Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Search(string text)
        {
            ActionExecutingContext filterContext = new ActionExecutingContext();

            filterContext.Result = new JsonResult
            {
                Data = ADUsers.Search(text),
                ContentEncoding = System.Text.Encoding.UTF8,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return filterContext.Result;
        }
    }
}