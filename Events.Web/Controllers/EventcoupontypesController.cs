
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
    public class EventcoupontypesController : Controller
    {
        private readonly EventDbContext _context;

        public EventcoupontypesController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventcoupontypes
        public async Task<IActionResult> Index()
        {
            var eventDbContext = _context.Eventcoupontypes.Include(e => e.CreatedByNavigation).Include(e => e.Event).Include(e => e.ModifiedByNavigation);
            return View(await eventDbContext.ToListAsync());
        }

        // GET: Eventcoupontypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Eventcoupontypes == null)
            {
                return NotFound();
            }

            var eventcoupontype = await _context.Eventcoupontypes
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventcoupontype == null)
            {
                return NotFound();
            }

            return View(eventcoupontype);
        }

        // GET: Eventcoupontypes/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return View();
        }

        // POST: Eventcoupontypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,CouponName,CouponPrice,Active,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Eventcoupontype eventcoupontype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventcoupontype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcoupontype.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.ModifiedBy);
            return View(eventcoupontype);
        }

        // GET: Eventcoupontypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventcoupontypes == null)
            {
                return NotFound();
            }

            var eventcoupontype = await _context.Eventcoupontypes.FindAsync(id);
            if (eventcoupontype == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcoupontype.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.ModifiedBy);
            return View(eventcoupontype);
        }

        // POST: Eventcoupontypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EventId,CouponName,CouponPrice,Active,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Eventcoupontype eventcoupontype)
        {
            if (id != eventcoupontype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventcoupontype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventcoupontypeExists(eventcoupontype.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcoupontype.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcoupontype.ModifiedBy);
            return View(eventcoupontype);
        }

        // GET: Eventcoupontypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Eventcoupontypes == null)
            {
                return NotFound();
            }

            var eventcoupontype = await _context.Eventcoupontypes
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventcoupontype == null)
            {
                return NotFound();
            }

            return View(eventcoupontype);
        }

        // POST: Eventcoupontypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventcoupontypes == null)
            {
                return Problem("Entity set 'EventDbContext.Eventcoupontypes'  is null.");
            }
            var eventcoupontype = await _context.Eventcoupontypes.FindAsync(id);
            if (eventcoupontype != null)
            {
                _context.Eventcoupontypes.Remove(eventcoupontype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventcoupontypeExists(long id)
        {
          return _context.Eventcoupontypes.Any(e => e.Id == id);
        }
    }
}
