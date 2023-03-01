using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using Events.Web.eventcontext;

namespace Events.Web.Controllers
{
    public class EventsponsorsController : Controller
    {
        private readonly EventDbContext _context;

        public EventsponsorsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventsponsors
        public async Task<IActionResult> Index()
        {
            var eventDbContext = _context.Eventsponsors.Include(e => e.CreatedByNavigation).Include(e => e.Event).Include(e => e.ModifiedByNavigation);
            return View(await eventDbContext.ToListAsync());
        }

        // GET: Eventsponsors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eventsponsors == null)
            {
                return NotFound();
            }

            var eventsponsor = await _context.Eventsponsors
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventsponsor == null)
            {
                return NotFound();
            }

            return View(eventsponsor);
        }

        // GET: Eventsponsors/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return View();
        }

        // POST: Eventsponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,SponsorName,SponsorOrganization,AmountSponsored,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Eventsponsor eventsponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventsponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsor.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.ModifiedBy);
            return View(eventsponsor);
        }

        // GET: Eventsponsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eventsponsors == null)
            {
                return NotFound();
            }

            var eventsponsor = await _context.Eventsponsors.FindAsync(id);
            if (eventsponsor == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsor.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.ModifiedBy);
            return View(eventsponsor);
        }

        // POST: Eventsponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,SponsorName,SponsorOrganization,AmountSponsored,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Eventsponsor eventsponsor)
        {
            if (id != eventsponsor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsponsorExists(eventsponsor.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsor.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventsponsor.ModifiedBy);
            return View(eventsponsor);
        }

        // GET: Eventsponsors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventsponsors == null)
            {
                return NotFound();
            }

            var eventsponsor = await _context.Eventsponsors
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventsponsor == null)
            {
                return NotFound();
            }

            return View(eventsponsor);
        }

        // POST: Eventsponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eventsponsors == null)
            {
                return Problem("Entity set 'EventDbContext.Eventsponsors'  is null.");
            }
            var eventsponsor = await _context.Eventsponsors.FindAsync(id);
            if (eventsponsor != null)
            {
                _context.Eventsponsors.Remove(eventsponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsponsorExists(int id)
        {
          return _context.Eventsponsors.Any(e => e.Id == id);
        }
    }
}
