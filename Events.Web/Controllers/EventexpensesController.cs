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
    public class EventexpensesController : Controller
    {
        private readonly EventDbContext _context;

        public EventexpensesController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventexpenses
        public async Task<IActionResult> Index()
        {
            var eventDbContext = _context.Eventexpenses.Include(e => e.CreatedByNavigation).Include(e => e.Event).Include(e => e.ModifiedByNavigation);
            return View(await eventDbContext.ToListAsync());
        }

        // GET: Eventexpenses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Eventexpenses == null)
            {
                return NotFound();
            }

            var eventexpense = await _context.Eventexpenses
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventexpense == null)
            {
                return NotFound();
            }

            return View(eventexpense);
        }

        // GET: Eventexpenses/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return View();
        }

        // POST: Eventexpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseName,EventId,ExpenseSubject,AmountSpent,CreatedOn,CreatedBy,Remarks,ModifiedBy,ModifiedOn")] Eventexpense eventexpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventexpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventexpense.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.ModifiedBy);
            return View(eventexpense);
        }

        // GET: Eventexpenses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventexpenses == null)
            {
                return NotFound();
            }

            var eventexpense = await _context.Eventexpenses.FindAsync(id);
            if (eventexpense == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventexpense.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.ModifiedBy);
            return View(eventexpense);
        }

        // POST: Eventexpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ExpenseName,EventId,ExpenseSubject,AmountSpent,CreatedOn,CreatedBy,Remarks,ModifiedBy,ModifiedOn")] Eventexpense eventexpense)
        {
            if (id != eventexpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventexpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventexpenseExists(eventexpense.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventexpense.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id", eventexpense.ModifiedBy);
            return View(eventexpense);
        }

        // GET: Eventexpenses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Eventexpenses == null)
            {
                return NotFound();
            }

            var eventexpense = await _context.Eventexpenses
                .Include(e => e.CreatedByNavigation)
                .Include(e => e.Event)
                .Include(e => e.ModifiedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventexpense == null)
            {
                return NotFound();
            }

            return View(eventexpense);
        }

        // POST: Eventexpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventexpenses == null)
            {
                return Problem("Entity set 'EventDbContext.Eventexpenses'  is null.");
            }
            var eventexpense = await _context.Eventexpenses.FindAsync(id);
            if (eventexpense != null)
            {
                _context.Eventexpenses.Remove(eventexpense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventexpenseExists(long id)
        {
          return _context.Eventexpenses.Any(e => e.Id == id);
        }
    }
}
