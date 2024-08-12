using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Session["User"] == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Account"));
            }
        }
    }
}