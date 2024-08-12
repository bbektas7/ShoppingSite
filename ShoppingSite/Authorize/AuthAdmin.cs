using Shopping.Entity.Models;
using ShoppingSite.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Authorize
{
    public class AuthAdmin : FilterAttribute
    {
        public void OnAuthhorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                var user = HttpContext.Current.Session["User"] as User;
                if (!user.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }

    }
}