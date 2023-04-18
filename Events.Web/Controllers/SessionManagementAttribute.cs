//using Microsoft.AspNetCore.Authorization.Infrastructure;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;

//namespace Events.Web.Controllers
//{

//    public class SessionManagementAttribute : TypeFilterAttribute
//    {
//        public SessionManagementAttribute() : base(typeof(ClaimsAuthorizationRequirement)) { }

//    }
//    public class ClaimsAuthorizationRequirement : ActionFilterAttribute
//    {
//        private readonly IHttpContextAccessor cd;
//        public  void OnActionExecuting(ActionExecutingContext context)
//        {

//            if (cd.HttpContext.Session.Id == null)
//            {
//                //context.Result = new RedirectResult("~/Controllers/Account/Login");
//                return;
//            }
//            base.OnActionExecuting(context);
//        }
//    }
//}
