using Events.Business;
using Events.Common;
using Events.DomainObjects;
using Events.Services;
using Events.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Events.Web.Services.CommonService;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        #region Variables

        private readonly IEventsService _eventsService;
        internal readonly IViewRenderingService _renderingService;
        private readonly ILogger<AccountController> _logger;
        internal readonly IActionContextAccessor _actionContextAccessor;
        private readonly EventDbContext _db;
        private readonly IHttpContextAccessor cd;
        private static Int64 MID;
        #endregion

        #region Constructors

        public EventsController(IViewRenderingService viewrenderingservice,
                                ILogger<AccountController> logger,
                                IActionContextAccessor actionContextAccessor,
                                IEventsService eventsService,
                                EventDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _eventsService = eventsService;
            _logger = logger;
            _actionContextAccessor = actionContextAccessor;
            _renderingService = viewrenderingservice;
            _db = db;
            cd = httpContextAccessor;
        }


        #endregion

        #region Event Methods

        // GET: /<controller>/

        public IActionResult Index()
        {

            string mid= cd.HttpContext.Session.GetString("MID");
            MID=Convert.ToInt64(mid); 
            return View();
        }
        public ActionResult GetEventList(JqueryDatatableParam param)
        {
            var events = _db.Events.ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                events = events.Where(x => x.EventName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.EventDate.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventStartTime.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventEndTime.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventStatus.ToLower().Contains(param.sSearch.ToLower())
                                              || x.EventVenue.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            ////Sorting
            if (param.iSortCol_0 == 0)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventName).ToList() : events.OrderByDescending(c => c.EventName).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventDate).ToList() : events.OrderByDescending(c => c.EventDate).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventStartTime).ToList() : events.OrderByDescending(c => c.EventStartTime).ToList();

            }
            else if (param.iSortCol_0 == 3)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventEndTime).ToList() : events.OrderByDescending(c => c.EventEndTime).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventStatus).ToList() : events.OrderByDescending(c => c.EventStatus).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                events = param.sSortDir_0 == "asc" ? events.OrderBy(c => c.EventVenue).ToList() : events.OrderByDescending(c => c.EventVenue).ToList();
            }

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
        public IActionResult CreateEvents(Event events)
        {
            if (events.Id == 0 || events.Id==null)
            {
                string mid = cd.HttpContext.Session.GetString("MID");
                var eventt = new Event()
                {
                    EventName = events.EventName,
                    EventDate = events.EventDate,
                    EventVenue = events.EventVenue,
                    EventStartTime = events.EventStartTime,
                    EventEndTime = events.EventEndTime,
                    EventStatus = events.EventStatus,
                    EventYear = events.EventDate,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy= Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                    FoodMenu = events.FoodMenu,
                };
                _db.Events.Add(eventt);
                _db.SaveChanges();
                return Json("Event is Created...");
            }
            else
            {
                string mid = cd.HttpContext.Session.GetString("MID");
                var eventt =_db.Events.Where(m => m.Id == events.Id).FirstOrDefault();


                eventt.EventName = events.EventName;
                eventt.EventDate = events.EventDate;
                eventt.EventVenue = events.EventVenue;
                eventt.EventStartTime = events.EventStartTime;
                eventt.EventEndTime = events.EventEndTime;
                eventt.EventStatus = events.EventStatus;
                eventt.EventYear = events.EventDate;
                eventt.ModifiedBy = Convert.ToInt64(mid);
                eventt.ModifiedOn = DateTime.Now;
                eventt.FoodMenu = events.FoodMenu;
                
                _db.Events.Update(eventt);
                _db.SaveChanges();
                return Json("Event Updated");
            }
         
        }

        public IActionResult Edit(Int64 id)
        {
            var EC = _db.Events.Where(x => x.Id == id).FirstOrDefault();
            return Json(EC);
        }
        
        public IActionResult DeteleEvent(Int64 id)
        {
            var data = _db.Events.Where(e => e.Id == id).SingleOrDefault();
            _db.Events.Remove(data);
            _db.SaveChanges();
            return Json("success");
        }
        
        public IActionResult EventsDetails(Int64 id)
        {
            var EC = _db.Events.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_EventsDetails", EC);
        }


        #endregion

        #region Event Expense Methods

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
        public ActionResult Admin()
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            Int64 id = Int16.Parse(mid);
            var i = _db.Users.Where(e => e.Id == id).FirstOrDefault();
            if (i.Role == "Admin")
            {
                return Json("true");
            }
            return Json("false");
        }
        #endregion
    }
}

