using Events.Business;
using Events.Services;
using Events.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySqlX.XDevAPI.Common;

namespace Events.Web.Session
{

    public class SessionTimeoutAttribute : IActionFilter
    {
        private readonly IHttpContextAccessor cd;

        
        public SessionTimeoutAttribute( IHttpContextAccessor httpContextAccessor)
        {
            cd = httpContextAccessor;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }
     

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (cd.HttpContext.Session.GetString("MID") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
              { "controller", "Account" },
              { "action", "login" }
                 });
            }
        }
    }
}
    