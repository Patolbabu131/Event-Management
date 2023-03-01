using Events.Business;
using Events.Common;
using Events.DomainObjects;
using Events.Services;
using Events.Web.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Events.Web.Services.CommonService;
using Events.Web.eventcontext;

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
          private readonly EventDbContext _db;

        #endregion

        #region Constructors

        public EventsController(IViewRenderingService viewrenderingservice,
                                ILogger<AccountController> logger,
                                IActionContextAccessor actionContextAccessor,
                                IEventsService eventsService,
                                EventDbContext db)
        {
            _eventsService = eventsService;
            _logger = logger;
            _actionContextAccessor = actionContextAccessor;
            _renderingService = viewrenderingservice;
            _db = db;
        }


        #endregion

        #region Event Methods

        // GET: /<controller>/
        public IActionResult Index()
        {
         
            return View();
        }

        //public ContentResult GetEventList()
        //{
        //    var data = string.Empty;

        //    try
        //    {
        //        var finallist = new List<EventsResponse>();
        //        var parameters = GetParameters(new List<string>() { "", "Year", "", "", "", "", "" });
         
        //        //Call the Business Method to Fetch the Data from Database.
        //        finallist = _eventsService.GetEventsList(parameters);
        //        data = Serialization.Json.Serialize(new { iTotalRecords = 0, iTotalDisplayRecords = 0, aaData = new string[] { } });

        //    }
        //    catch (Exception)
        //    {
        //        data = Serialization.Json.Serialize(new
        //        {
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new string[] { }
        //        });
        //    }

        //    return Content(data, "application/json");
        //}

        public ActionResult GetEventList(JqueryDatatableParam param)
        {
            var events =_db.Events.ToList();

            //Searching
            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //    schools = schools.Where(x => x.SId.ToString().Contains(param.sSearch.ToLower())
            //                                  || x.SName.ToString().Contains(param.sSearch.ToLower())
            //                                  || x.SAddress.ToString().Contains(param.sSearch.ToLower())
            //                                  || x.SCity.ToString().Contains(param.sSearch.ToLower())
            //                                  || x.SState.ToString().Contains(param.sSearch.ToLower())).ToList();
            //}
            ////Sorting
            //if (param.iSortCol_0 == 0)
            //{
            //    schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SId).ToList() : schools.OrderByDescending(c => c.SId).ToList();
            //}
            //else if (param.iSortCol_0 == 1)
            //{
            //    schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SName).ToList() : schools.OrderByDescending(c => c.SName).ToList();
            //}
            //else if (param.iSortCol_0 == 2)
            //{
            //    schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SAddress).ToList() : schools.OrderByDescending(c => c.SAddress).ToList();
            //}
            //else if (param.iSortCol_0 == 3)
            //{
            //    schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SCity).ToList() : schools.OrderByDescending(c => c.SCity).ToList();
            //}
            //else if (param.iSortCol_0 == 4)
            //{
            //    schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SState).ToList() : schools.OrderByDescending(c => c.SState).ToList();
            //}

            //TotalRecords
            var displayResult = events.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = events.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        public IActionResult CreateEvent()
        {
            return PartialView("_AddEditEvent");
        }
        [HttpPost]
        public IActionResult CreateEvents(Event @event)
        {
            return PartialView("_AddEditEvent");
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

