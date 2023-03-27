using Events.Business;
using Events.Common;
using Events.DomainObjects;
using Events.Services;
using Events.Web.Models;
using System.Web;
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

        //public ActionResult GetEventattendees(JqueryDatatableParam param, Int64 Id)
        //{
        //    IEnumerable<dynamic> eventattendees = null;
        //    if (Id == null || Id == 0)
        //    {
        //        eventattendees = _db.Eventattendees.ToList();
        //    }
        //    else
        //    {
        //        eventattendees = _db.Eventattendees.Where(m => m.EventId == Id);
        //    }

        //    //Searching
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        eventattendees = eventattendees.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.EventId.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.AttendeeName.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.ContactNo.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.CouponsPurchased.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.PurchasedOn.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.TotalAmount.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.Remarks.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.InvitedBy.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.CouponTypeId.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.RemainingCoupons.ToString().Contains(param.sSearch.ToLower())
        //                                      || x.ModeOfPayment.ToString().Contains(param.sSearch.ToLower())).ToList();
        //    }
        //    //Sorting
        //    if (param.iSortCol_0 == 0)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Id).ToList() : eventattendees.OrderByDescending(c => c.Id).ToList();
        //    }
        //    else if (param.iSortCol_0 == 1)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.EventId).ToList() : eventattendees.OrderByDescending(c => c.EventId).ToList();
        //    }
        //    else if (param.iSortCol_0 == 2)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.AttendeeName).ToList() : eventattendees.OrderByDescending(c => c.AttendeeName).ToList();

        //    }
        //    else if (param.iSortCol_0 == 3)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ContactNo).ToList() : eventattendees.OrderByDescending(c => c.ContactNo).ToList();
        //    }
        //    else if (param.iSortCol_0 == 4)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.CouponsPurchased).ToList() : eventattendees.OrderByDescending(c => c.CouponsPurchased).ToList();
        //    }
        //    else if (param.iSortCol_0 == 5)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.TotalAmount).ToList() : eventattendees.OrderByDescending(c => c.TotalAmount).ToList();
        //    }
        //    else if (param.iSortCol_0 == 6)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Remarks).ToList() : eventattendees.OrderByDescending(c => c.Remarks).ToList();
        //    }
        //    else if (param.iSortCol_0 == 7)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.InvitedBy).ToList() : eventattendees.OrderByDescending(c => c.InvitedBy).ToList();
        //    }
        //    else if (param.iSortCol_0 == 8)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.CouponTypeId).ToList() : eventattendees.OrderByDescending(c => c.CouponTypeId).ToList();
        //    }
        //    else if (param.iSortCol_0 == 9)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.RemainingCoupons).ToList() : eventattendees.OrderByDescending(c => c.RemainingCoupons).ToList();
        //    }
        //    else if (param.iSortCol_0 == 10)
        //    {
        //        eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ModeOfPayment).ToList() : eventattendees.OrderByDescending(c => c.ModeOfPayment).ToList();
        //    }



        //    //TotalRecords
        //    var displayResult = eventattendees.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
        //    var totalRecords = eventattendees.Count();
        //    return Json(new
        //    {
        //        param.sEcho,
        //        iTotalRecords = totalRecords,
        //        iTotalDisplayRecords = totalRecords,
        //        aaData = displayResult
        //    });
        //}
        public ActionResult GetEventList(JqueryDatatableParam param)
        {
            var events = _db.Events.ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                events = events.Where(x => x.EventName.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventDate.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventStartTime.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventEndTime.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventVenue.ToString().Contains(param.sSearch.ToLower())).ToList();
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
                    EventYear = events.EventDate,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy= Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                    FoodMenu = "nothing"
                };
                _db.Events.Add(eventt);
                _db.SaveChanges();
                return Json("Event is Created...");
            }
            else
            {
                string mid = cd.HttpContext.Session.GetString("MID");
                var eventt = new Event()
                {
                    Id=events.Id,
                    EventName = events.EventName,
                    EventDate = events.EventDate,
                    EventVenue = events.EventVenue,
                    EventStartTime = events.EventStartTime,
                    EventEndTime = events.EventEndTime,
                    EventYear = events.EventDate,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                    FoodMenu = "nothing"
                };
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

        #endregion
    }
}

