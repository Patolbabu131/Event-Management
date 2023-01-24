using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Services;
using Events.Web.Models.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using static Events.Web.Services.CommonService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class EventsController : BaseController
    {
        #region Variables



        #endregion

        #region Constructors

        public EventsController(IViewRenderingService viewrenderingservice,
                                ILogger<AccountController> logger,
                                IActionContextAccessor actionContextAccessor)
                                : base(viewrenderingservice, logger, actionContextAccessor) { }

        #endregion

        #region Methods

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method to Fetch the 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEventExpenses()
        {
            var result = new GenericJsonResponse<EventExpenseModel>();
            try
            {

                var expensemodel = new EventExpenseModel();

                string stringExpenseModel = string.Empty;
                stringExpenseModel = _renderingService.RenderPartialView(_actionContextAccessor.ActionContext, "~/Views/Events/_EventExpenses.cshtml", expensemodel);
                result.IsSucceed = true;

                return Json(new { result, stringExpenseModel });
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}

