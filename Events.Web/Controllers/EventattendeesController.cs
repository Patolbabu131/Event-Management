using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventattendeesController : Controller
    {
        public class ViewModel
        {
            public Eventcouponassignment eventcouponassignments { get; set; }
            public Eventcoupontype coupontype { get; set; }
        }
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;

        public EventattendeesController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd = httpContextAccessor;
        }
    
        // GET: Eventattendees
        public async Task<IActionResult> Index(Int64 Id)
        {

            if (Id == null || Id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.VBFriend = _context.Events.Where(e => e.Id == Id).FirstOrDefault();
                ViewBag.Eid = Id;
                return View();
            }

        }

        public ActionResult GetEventattendees(JqueryDatatableParam param, Int64 Id)
        {
            // var list = _context.Eventattendees.ToList();
            IEnumerable<dynamic> eventattendees = null;
            if (Id == null || Id == 0)
            {
                eventattendees = _context.Eventattendees.ToList();
            }
            else
            {
                eventattendees = _context.Eventattendees.Where(e => e.EventId == Id).ToList();
            }

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                eventattendees = eventattendees.Where(x => x.AttendeeName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.ContactNo.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsPurchased.ToString().Contains(param.sSearch.ToLower())
                                              || x.PurchasedOn.ToString().Contains(param.sSearch.ToString())
                                              || x.PaymentStatus.ToString().ToLower().Contains(param.sSearch.ToLower())
                                              || x.ModeOfPayment.ToString().ToLower().Contains(param.sSearch.ToLower())
                                              || x.PaymentReference.ToString().Contains(param.sSearch.ToString())).ToList();
            }

            //Sorting
            if (param.iSortCol_0 == 0)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.AttendeeName).ToList() : eventattendees.OrderByDescending(c => c.AttendeeName).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ContactNo).ToList() : eventattendees.OrderByDescending(c => c.ContactNo).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.CouponsPurchased).ToList() : eventattendees.OrderByDescending(c => c.CouponsPurchased).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.PurchasedOn).ToList() : eventattendees.OrderByDescending(c => c.PurchasedOn).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.PaymentStatus).ToList() : eventattendees.OrderByDescending(c => c.PaymentStatus).ToList();
            }
            else if (param.iSortCol_0 == 5)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ModeOfPayment).ToList() : eventattendees.OrderByDescending(c => c.ModeOfPayment).ToList();
            }
            else if (param.iSortCol_0 == 6)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.PaymentReference).ToList() : eventattendees.OrderByDescending(c => c.PaymentReference).ToList();
            }
            //else if (param.iSortCol_0 == 5)
            //{
            //    eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ModeOfPayment).ToList() : eventattendees.OrderByDescending(c => c.Remarks).ToList();
            //}

            else if (param.iSortCol_0 == 7)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Remarks).ToList() : eventattendees.OrderByDescending(c => c.Remarks).ToList();
            }

            //TotalRecords
            var displayResult = eventattendees.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = eventattendees.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Eventattendees == null)
            {
                return NotFound();
            }

            var eventattendee = await _context.Eventattendees
                .Include(e => e.CouponType)
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ExecutiveMemberNavigation)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventattendee == null)
            {
                return NotFound();
            }

            return View(eventattendee);
        }
        public IActionResult CreateEdit(Int64 id)
        {

            ViewBag.eid = id;

            ViewData["ExecutiveMember"] = new SelectList(_context.Executivemembers, "Id", "FullName");

            return PartialView("CreateEdit");
        }

        public IActionResult Edit(Int64 id)
        {
            var Eventattendees = _context.Eventattendees.Where(inc => inc.Id == id).FirstOrDefault();


            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes.Where(c => c.EventId == Eventattendees.EventId), "Id", "CouponName");
            ViewData["ExecutiveMember"] = new SelectList(_context.Eventattendees, "Id", "Id");


            var s = _context.Eventcoupontypes.Where(e => e.Id == Eventattendees.CouponTypeId).FirstOrDefault();

            ViewBag.couponname = s.CouponName;

            return PartialView("CreateEdit");
        }

        public IActionResult Edit1(Int64 id)
        {
            var Eventattendees = _context.Eventattendees.Where(inc => inc.Id == id).FirstOrDefault();
            return Json(Eventattendees);
        }

        public IActionResult fetchcoupon(Int64 id)
        {
            EventDbContext entities = new EventDbContext();
            var myvalues = (from values in entities.Eventcouponassignmentmappings
                            where values.ExecutiveMember != null
                            select values.CouponTypeId).ToArray();

            IEnumerable<long> uniqueItems = myvalues.Distinct<long>();

            List<Eventcoupontype> coupon = new List<Eventcoupontype>();
            foreach (Int64 i in uniqueItems)
            {
                List<Eventcoupontype> eventcoupontypes = _context.Eventcoupontypes.Where(e => e.Id == i).ToList();
                coupon.AddRange(eventcoupontypes);
            }

            return Json(coupon);
        }

        public IActionResult getnoofcoupon(Int64 id, Int64 mid, Int64 aid)

        {

            if (aid == null || aid == 0)
            {
                var nocoupon = _context.Eventcouponassignmentmappings.Where(e => (e.CouponTypeId == id && e.ExecutiveMember == mid) && e.Booked == "false").ToList();
                return Json(nocoupon);
            }
            else
            {
                var nocoupon = _context.Eventcouponassignmentmappings.Where(e => (e.CouponTypeId == id && e.ExecutiveMember == mid) || e.Attendee == aid).ToList();
                return Json(nocoupon);
            }
            return Json("somthing went wrong");
        }

        [HttpPost]
        public IActionResult CreateEdit1(Eventattendee eventattendee)
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            if (eventattendee.Id == null || eventattendee.Id == 0)
            {
                var member = new Eventattendee()
                {
                    EventId = eventattendee.EventId,
                    AttendeeName = eventattendee.AttendeeName,
                    ContactNo = eventattendee.ContactNo,
                    CouponsPurchased = eventattendee.CouponsPurchased,
                    PurchasedOn = eventattendee.PurchasedOn,
                    ExecutiveMember = eventattendee.ExecutiveMember,
                    CouponTypeId = eventattendee.CouponTypeId,
                    TotalAmount = 100,
                    ModeOfPayment = eventattendee.ModeOfPayment,
                    PaymentReference = eventattendee.PaymentReference,
                    PaymentStatus = eventattendee.PaymentStatus,
                    Remarks = eventattendee.Remarks,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                };
                _context.Eventattendees.Add(member);
                _context.SaveChanges();

                foreach (var i in eventattendee.Eventcouponassignmentmappings)
                {
                    var multiplecoupon = _context.Eventcouponassignmentmappings.Where(e => e.Id == i.Id).FirstOrDefault();
                    multiplecoupon.Attendee = member.Id;
                    multiplecoupon.Booked = i.Booked;
                    _context.Eventcouponassignmentmappings.Update(multiplecoupon);
                }
                _context.SaveChanges();
                return Json("Event created...");
            }
            else
            {
                var attendees = _context.Eventattendees.Where(m => m.Id == eventattendee.Id).FirstOrDefault();

                attendees.AttendeeName = eventattendee.AttendeeName;
                attendees.ContactNo = eventattendee.ContactNo;
                attendees.PurchasedOn = eventattendee.PurchasedOn;
                attendees.ExecutiveMember = eventattendee.ExecutiveMember;
                attendees.CouponTypeId = eventattendee.CouponTypeId;
                attendees.TotalAmount = eventattendee.TotalAmount;
                attendees.PaymentReference = eventattendee.PaymentReference;
                attendees.PaymentStatus = eventattendee.PaymentStatus;
                attendees.Remarks = eventattendee.Remarks;
                attendees.CouponTypeId = eventattendee.CouponTypeId;
                attendees.ModifiedBy = Convert.ToInt64(mid);
                attendees.ModifiedOn = DateTime.Now;
                attendees.ModeOfPayment = eventattendee.ModeOfPayment;

                var names1 = attendees.CouponsPurchased.Split(',').Select(int.Parse).ToList();
                var names2 = eventattendee.CouponsPurchased.Split(',').Select(int.Parse).ToList();

                foreach (var i in names1)
                {
                    var multiplecoupon = _context.Eventcouponassignmentmappings.Where(e => e.Id == i).FirstOrDefault();
                    multiplecoupon.Attendee = null;
                    multiplecoupon.Booked = "false";
                    _context.Eventcouponassignmentmappings.Update(multiplecoupon);
                }
                _context.SaveChanges();

                foreach (var i in names2)
                {
                    var multiplecoupon = _context.Eventcouponassignmentmappings.Where(e => e.Id == i).FirstOrDefault();
                    multiplecoupon.Attendee = eventattendee.Id;
                    multiplecoupon.Booked = "1";
                    _context.Eventcouponassignmentmappings.Update(multiplecoupon);
                }
                _context.SaveChanges();

                attendees.CouponsPurchased = eventattendee.CouponsPurchased;
                _context.Eventattendees.Update(attendees);
                _context.SaveChanges();


                return Json("Event updated...");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Eventattendee eventattendee)
        {
            if (id != eventattendee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventattendee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventattendeeExists(eventattendee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventattendee.CouponTypeId);
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventattendee.EventId);
            ViewData["ExecutiveMember"] = new SelectList(_context.Eventattendees, "Id", "Id", eventattendee.ExecutiveMember);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.ModifiedBy);
            return View(eventattendee);
        }

        public IActionResult Delete(long? id)
        {
            var attendees = _context.Eventattendees.Where(e => e.Id == id).FirstOrDefault();

            _context.Eventattendees.Remove(attendees);
            var multiplecoupon = _context.Eventcouponassignmentmappings.Where(e => e.Attendee == id).ToList();

            foreach (var i in multiplecoupon)
            {
                var multiplecoupon1 = _context.Eventcouponassignmentmappings.Where(e => e.Id == i.Id).FirstOrDefault();
                multiplecoupon1.Attendee = null;
                multiplecoupon1.Booked = "false";
                _context.Eventcouponassignmentmappings.Update(multiplecoupon1);
            }
            //_context.SaveChanges();
            _context.SaveChanges();
            return Json("success");
        }

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventattendees == null)
            {
                return Problem("Entity set 'EventDbContext.Eventattendees'  is null.");
            }
            var eventattendee = await _context.Eventattendees.FindAsync(id);
            if (eventattendee != null)
            {
                _context.Eventattendees.Remove(eventattendee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventattendeeExists(long id)
        {
            return _context.Eventattendees.Any(e => e.Id == id);
        }
    }
}
