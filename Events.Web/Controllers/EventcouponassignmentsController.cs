using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;


namespace Events.Web.Controllers
{
    public class EventcouponassignmentsController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;
        public EventcouponassignmentsController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd=httpContextAccessor;
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
                Cassign = Cassign.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventId.ToString().Contains(param.sSearch.ToLower())
                                              || x.ExecutiveMemberId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsFrom.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsTo.ToString().Contains(param.sSearch.ToLower())
                                              || x.TotalCoupons.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedOn.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedOn.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            else if (param.iSortCol_0 == 0)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.Id).ToList() : Cassign.OrderByDescending(c => c.Id).ToList();
            }
            if (param.iSortCol_0 == 1)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.EventId).ToList() : Cassign.OrderByDescending(c => c.EventId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.ExecutiveMemberId).ToList() : Cassign.OrderByDescending(c => c.ExecutiveMemberId).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CouponsFrom).ToList() : Cassign.OrderByDescending(c => c.CouponsFrom).ToList();

            }
            else if (param.iSortCol_0 == 4)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CouponsTo).ToList() : Cassign.OrderByDescending(c => c.CouponsTo).ToList();
            }
            else if (param.iSortCol_0 == 5)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.TotalCoupons).ToList() : Cassign.OrderByDescending(c => c.TotalCoupons).ToList();
            }
            else if (param.iSortCol_0 == 6)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CreatedOn).ToList() : Cassign.OrderByDescending(c => c.CreatedOn).ToList();
            }
            else if (param.iSortCol_0 == 7)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.CreatedBy).ToList() : Cassign.OrderByDescending(c => c.CreatedBy).ToList();
            }
            else if (param.iSortCol_0 == 8)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.ModifiedBy).ToList() : Cassign.OrderByDescending(c => c.ModifiedBy).ToList();
            }
            else if (param.iSortCol_0 == 9)
            {
                Cassign = param.sSortDir_0 == "asc" ? Cassign.OrderBy(c => c.ModifiedOn).ToList() : Cassign.OrderByDescending(c => c.ModifiedOn).ToList();

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
        // GET: Eventcouponassignments/Create
        public IActionResult CreateCAssign(Int64 id)
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewBag.eid = id;
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "Id");
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
                    ExecutiveMemberId = Convert.ToInt64(mid),
                    CouponsFrom = eventcouponassignment.CouponsFrom,
                    CouponsTo = eventcouponassignment.CouponsTo,
                    TotalCoupons = eventcouponassignment.TotalCoupons,
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


                var member = new Eventcouponassignment()
                {
                    Id=eventcouponassignment.Id,
                    EventId = eventcouponassignment.EventId,
                    ExecutiveMemberId = Convert.ToInt64(mid),
                    CouponsFrom = eventcouponassignment.CouponsFrom,
                    CouponsTo = eventcouponassignment.CouponsTo,
                    TotalCoupons = eventcouponassignment.TotalCoupons,
                    CreatedOn = eventcouponassignment.CreatedOn,
                    CreatedBy = eventcouponassignment.CreatedBy,
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                };
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
        
            // POST: Eventcouponassignments/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Eventcouponassignments/Edit/5
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

        // POST: Eventcouponassignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Eventcouponassignments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var data = _context.Eventcouponassignments.Where(e => e.Id == id).SingleOrDefault();
            _context.Eventcouponassignments.Remove(data);
            _context.SaveChanges();
            return Json("success");
        }

        // POST: Eventcouponassignments/Delete/5
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
