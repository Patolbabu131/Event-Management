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

        public EventcouponassignmentsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventcouponassignments
        public async Task<IActionResult> Index()
        {
            var eventDbContext = _context.Eventcouponassignments.Include(e => e.CreatedByNavigation).Include(e => e.Event).Include(e => e.ExecutiveMember).Include(e => e.ModifiedByNavigation);
            return View(await eventDbContext.ToListAsync());
        }

        // GET: Eventcouponassignments/Details/5
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

        // GET: Eventcouponassignments/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["ExecutiveMemberId"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return View();
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
