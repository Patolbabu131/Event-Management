using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using Org.BouncyCastle.Asn1.X500;
using System.Collections;


namespace Events.Web.Controllers
{
    public class EventcouponassignmentsController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;
        public EventcouponassignmentsController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd = httpContextAccessor;
        }

        // GET: Eventcouponassignments
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

        public ActionResult GetCAssign(JqueryDatatableParam param, Int64 Id)
        {
            IEnumerable<dynamic> Cassign = null;

            if (Id == null || Id == 0)
            {
                Cassign = _context.Eventcouponassignments.ToList();
            }
            else
            {
                Cassign = _context.Eventcouponassignments.Where(m => m.EventId == Id);
            }
            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                Cassign = Cassign.Where(x => x.ExecutiveMemberId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponTypeId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsFrom.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsTo.ToString().Contains(param.sSearch.ToLower())
                                              || x.TotalCoupons.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.ExecutiveMemberId).ToList() : Cassign.OrderByDescending(c => c.ExecutiveMemberId).ToList();
            }
            if (param.iSortCol_0 == 1)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CouponTypeId).ToList() : Cassign.OrderByDescending(c => c.CouponTypeId).ToList();
            }
            if (param.iSortCol_0 == 2)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CouponsFrom).ToList() : Cassign.OrderByDescending(c => c.CouponsFrom).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CouponsTo).ToList() : Cassign.OrderByDescending(c => c.CouponsTo).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.TotalCoupons).ToList() : Cassign.OrderByDescending(c => c.TotalCoupons).ToList();
            }


            //TotalRecords
            var displayResult = Cassign.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = Cassign.Count();
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
            if (id == null || _context.Eventcouponassignments == null)
            {
                return NotFound();
            }

            var eventcouponassignment = await _context.Eventcouponassignments
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ExecutiveMember)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventcouponassignment == null)
            {
                return NotFound();
            }

            return View(eventcouponassignment);
        }

        [HttpGet]
        public IActionResult CreateCAssign(Int64 id)
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "CouponName");
            ViewBag.eid = id;
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "FullName");
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return PartialView("Create");
        }
        [HttpPost]
        public IActionResult CreateCAssign(Eventcouponassignment eventcouponassignment)
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            if (eventcouponassignment.Id == null || eventcouponassignment.Id == 0)
            {
               
                var member = new Eventcouponassignment()
                {
                    EventId = eventcouponassignment.EventId,
                    CouponTypeId=eventcouponassignment.CouponTypeId,
                    ExecutiveMemberId = eventcouponassignment.ExecutiveMemberId,
                    CouponsFrom = eventcouponassignment.CouponsFrom,
                    CouponsTo = eventcouponassignment.CouponsTo,
                    TotalCoupons =eventcouponassignment.TotalCoupons,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,

                };
                _context.Eventcouponassignments.Add(member);
                _context.SaveChanges();

                return Json("Coupon Assignments...");
            }
            else
            {


                var member = _context.Eventcouponassignments.Where(m => m.Id == eventcouponassignment.Id).FirstOrDefault();

                member.ExecutiveMemberId = Convert.ToInt64(mid);
                member.CouponTypeId = eventcouponassignment.CouponTypeId;
                member.CouponsFrom = eventcouponassignment.CouponsFrom;
                member.CouponsTo = eventcouponassignment.CouponsTo;
                member.TotalCoupons = eventcouponassignment.TotalCoupons;
                member.ModifiedBy = Convert.ToInt64(mid);
                member.ModifiedOn = DateTime.Now;

                _context.Eventcouponassignments.Update(member);
                _context.SaveChanges();

                return Json("Assigned Coupon Updated....");

            }
        
        }

        public async Task<IActionResult> getEdit(long? id)
        {

            var cassign = _context.Eventcouponassignments.Where(x => x.Id == id).FirstOrDefault();
            return Json(cassign);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,ExecutiveMemberId,CouponsFrom,CouponsTo,TotalCoupons,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Eventcouponassignment eventcouponassignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventcouponassignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcouponassignment.EventId);
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ExecutiveMemberId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ModifiedBy);
            return View(eventcouponassignment);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventcouponassignments == null)
            {
                return NotFound();
            }

            var eventcouponassignment = await _context.Eventcouponassignments.FindAsync(id);
            if (eventcouponassignment == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcouponassignment.EventId);
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ExecutiveMemberId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ModifiedBy);
            return View(eventcouponassignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EventId,ExecutiveMemberId,CouponsFrom,CouponsTo,TotalCoupons,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Eventcouponassignment eventcouponassignment)
        {
            if (id != eventcouponassignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventcouponassignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventcouponassignmentExists(eventcouponassignment.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcouponassignment.EventId);
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ExecutiveMemberId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignment.ModifiedBy);
            return View(eventcouponassignment);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var data = _context.Eventcouponassignments.Where(e => e.Id == id).SingleOrDefault();
            _context.Eventcouponassignments.Remove(data);
            _context.SaveChanges();
            return Json("success");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventcouponassignments == null)
            {
                return Problem("Entity set 'EventDbContext.Eventcouponassignments'  is null.");
            }
            var eventcouponassignment = await _context.Eventcouponassignments.FindAsync(id);
            if (eventcouponassignment != null)
            {
                _context.Eventcouponassignments.Remove(eventcouponassignment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventcouponassignmentExists(long id)
        {
          return _context.Eventcouponassignments.Any(e => e.Id == id);
        }
    }
}
