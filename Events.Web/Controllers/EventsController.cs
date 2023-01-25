using Events.Business;
using Events.Common;
using Events.DomainObjects;
using Events.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Events.Web.Services.CommonService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class EventsController : BaseController
    {
        #region Variables

        private readonly IEventsService _eventsService;
        internal readonly IViewRenderingService _renderingService;
        private readonly ILogger<AccountController> _logger;
        internal readonly IActionContextAccessor _actionContextAccessor;

        #endregion

        #region Constructors

        public EventsController(IViewRenderingService viewrenderingservice,
                                ILogger<AccountController> logger,
                                IActionContextAccessor actionContextAccessor,
                                IEventsService eventsService)
        {
            _eventsService = eventsService;
            _logger = logger;
            _actionContextAccessor = actionContextAccessor;
            _renderingService = viewrenderingservice;
        }


        #endregion

        #region Event Methods

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ContentResult GetEventList()
        {
            var data = string.Empty;

            try
            {
                var finallist = new List<EventsResponse>();
                var parameters = GetParameters(new List<string>() { "", "Year", "", "", "", "", "" });

                //Call the Business Method to Fetch the Data from Database.
                finallist = _eventsService.GetEventsList(parameters);

                data = Serialization.Json.Serialize(new { iTotalRecords = 0, iTotalDisplayRecords = 0, aaData = new string[] { } });

            }
            catch (Exception)
            {
                data = Serialization.Json.Serialize(new
                {
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new string[] { }
                });
            }

            return Content(data, "application/json");
        }

        #endregion

        #region Event Expense Methods

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

