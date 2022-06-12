using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Helper
{
    public class SessionControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //eğer session null veya oturumun sonlanmışsa account login sayfasına gider.
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Session == null)
            {
                filterContext.HttpContext.Response.Redirect("Admin/Account/Login");
            }
            else
            {
                var user = filterContext.HttpContext.Session.User();

                if (user == null || user.UserName == "admin")
                {

                    filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary{
                                                { "area", "Admin" },
                                           { "controller", "Account" },
                                          { "action", "Login" }
                                         });

                    //filterContext.HttpContext.Response.Redirect("/Account/Login");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
