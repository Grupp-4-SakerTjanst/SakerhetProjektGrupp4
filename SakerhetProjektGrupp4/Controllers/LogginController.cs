using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakerhetProjektGrupp4.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SakerhetProjektGrupp4.Controllers
{
    public class LogginController : Controller
    {
        // GET: Loggin
        public ActionResult Index()
        {

            return View();
        }
        public class CustomAuthorizeAttribute : AuthorizeAttribute
        {
            PersonalModel context = new PersonalModel(); // my entity  
            private readonly string[] allowedroles;
            public CustomAuthorizeAttribute(params string[] roles)
            {
                this.allowedroles = roles;
            }
            //protected override bool AuthorizeCore(HttpContextBase httpContext)
            //{
            //    bool authorize = false;
            //    foreach (var role in allowedroles)
            //    {
            //        var user = context.Roll.Where(m => m.UserID == GetUser.CurrentUser/* getting user form current context */ && m.Role == role &&
            //        m.IsActive == true); // checking active users with allowed roles.  
            //        if (user.Count() > 0)
            //        {
            //            authorize = true; /* return true if Entity has current user(active) with specific role */
            //        }
            //    }
            //    return authorize;
            //}
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}