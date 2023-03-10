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
    public class EventattendeesController : Controller
    {
        private readonly EventDbContext _context;

        public EventattendeesController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventattendees
        public ActionResult Index(Int64 Id)
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

        public ActionResult GetEventattendees(JqueryDatatableParam param,Int64 Id)
        {
            IEnumerable<dynamic> eventattendees =null;
            if (Id ==null || Id == 0)
            {
                eventattendees = _context.Eventattendees.ToList();
            }
            else
            {
                 eventattendees = _context.Eventattendees.Where(m=>m.EventId==Id);
            }

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                eventattendees = eventattendees.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventId.ToString().Contains(param.sSearch.ToLower())
                                              || x.AttendeeName.ToString().Contains(param.sSearch.ToLower())
                                              || x.ContactNo.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponsPurchased.ToString().Contains(param.sSearch.ToLower())
                                              || x.PurchasedOn.ToString().Contains(param.sSearch.ToLower())
                                              || x.TotalAmount.ToString().Contains(param.sSearch.ToLower())
                                              || x.Remarks.ToString().Contains(param.sSearch.ToLower())
                                              || x.InvitedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponTypeId.ToString().Contains(param.sSearch.ToLower())
                                              || x.RemainingCoupons.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModeOfPayment.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Id).ToList() : eventattendees.OrderByDescending(c => c.Id).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.EventId).ToList() : eventattendees.OrderByDescending(c => c.EventId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.AttendeeName).ToList() : eventattendees.OrderByDescending(c => c.AttendeeName).ToList();

            }
            else if (param.iSortCol_0 == 3)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ContactNo).ToList() : eventattendees.OrderByDescending(c => c.ContactNo).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.CouponsPurchased).ToList() : eventattendees.OrderByDescending(c => c.CouponsPurchased).ToList();
            }
            else if (param.iSortCol_0 == 5)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.TotalAmount).ToList() : eventattendees.OrderByDescending(c => c.TotalAmount).ToList();
            }
            else if (param.iSortCol_0 == 6)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Remarks).ToList() : eventattendees.OrderByDescending(c => c.Remarks).ToList();
            }
            else if (param.iSortCol_0 == 7)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.InvitedBy).ToList() : eventattendees.OrderByDescending(c => c.InvitedBy).ToList();
            }
            else if (param.iSortCol_0 == 8)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.CouponTypeId).ToList() : eventattendees.OrderByDescending(c => c.CouponTypeId).ToList();
            }
            else if (param.iSortCol_0 == 9)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.RemainingCoupons).ToList() : eventattendees.OrderByDescending(c => c.RemainingCoupons).ToList();
            }
            else if (param.iSortCol_0 == 10)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.ModeOfPayment).ToList() : eventattendees.OrderByDescending(c => c.ModeOfPayment).ToList();
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

        // GET: Eventattendees/Details/5
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
                .Include(e => e.InvitedByNavigation)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventattendee == null)
            {
                return NotFound();
            }

            return View(eventattendee);
        }

        // GET: Eventattendees/Create
        public IActionResult Create()
        {
            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["InvitedBy"] = new SelectList(_context.Eventattendees, "Id", "Id");
            return View();
        }

        // POST: Eventattendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Eventattendee eventattendee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventattendee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventattendee.CouponTypeId);
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventattendee.EventId);
            ViewData["InvitedBy"] = new SelectList(_context.Eventattendees, "Id", "Id", eventattendee.InvitedBy);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.ModifiedBy);
            return View(eventattendee);
        }

        // GET: Eventattendees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventattendees == null)
            {
                return NotFound();
            }

            var eventattendee = await _context.Eventattendees.FindAsync(id);
            if (eventattendee == null)
            {
                return NotFound();
            }
            ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventattendee.CouponTypeId);
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventattendee.EventId);
            ViewData["InvitedBy"] = new SelectList(_context.Eventattendees, "Id", "Id", eventattendee.InvitedBy);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.ModifiedBy);
            return View(eventattendee);
        }

        // POST: Eventattendees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["InvitedBy"] = new SelectList(_context.Eventattendees, "Id", "Id", eventattendee.InvitedBy);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventattendee.ModifiedBy);
            return View(eventattendee);
        }

        // GET: Eventattendees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Eventattendees == null)
            {
                return NotFound();
            }

            var eventattendee = await _context.Eventattendees
                .Include(e => e.CouponType)
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.InvitedByNavigation)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventattendee == null)
            {
                return NotFound();
            }

            return View(eventattendee);
        }

        // POST: Eventattendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
