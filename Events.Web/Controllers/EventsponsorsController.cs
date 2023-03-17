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
    public class EventsponsorsController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;
        public EventsponsorsController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd=httpContextAccessor;
        }

        // GET: Eventsponsors
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
        

        public ActionResult Getsponsors(JqueryDatatableParam param, Int64 Id)
        {
            IEnumerable<dynamic> sponsors = null;
            if (Id == null || Id == 0)
            {
                sponsors = _context.Eventsponsors.ToList();
            }
            else
            {
                sponsors = _context.Eventsponsors.Where(m => m.EventId == Id);
            }

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                sponsors = sponsors.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventId.ToString().Contains(param.sSearch.ToLower())
                                              || x.SponsorName.ToString().Contains(param.sSearch.ToLower())
                                              || x.SponsorOrganization.ToString().Contains(param.sSearch.ToLower())
                                              || x.AmountSponsored.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedOn.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedOn.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.Id).ToList() : sponsors.OrderByDescending(c => c.Id).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.EventId).ToList() : sponsors.OrderByDescending(c => c.EventId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.SponsorName).ToList() : sponsors.OrderByDescending(c => c.SponsorName).ToList();

            }
            else if (param.iSortCol_0 == 3)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.SponsorOrganization).ToList() : sponsors.OrderByDescending(c => c.SponsorOrganization).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.AmountSponsored).ToList() : sponsors.OrderByDescending(c => c.AmountSponsored).ToList();
            }
            else if (param.iSortCol_0 == 5)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.CreatedOn).ToList() : sponsors.OrderByDescending(c => c.CreatedOn).ToList();
            }
            else if (param.iSortCol_0 == 6)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.CreatedBy).ToList() : sponsors.OrderByDescending(c => c.CreatedBy).ToList();
            }
            else if (param.iSortCol_0 == 7)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.ModifiedBy).ToList() : sponsors.OrderByDescending(c => c.ModifiedBy).ToList();
            }
            else if (param.iSortCol_0 == 8)
            {
                sponsors = param.sSortDir_0 == "asc" ? sponsors.OrderBy(c => c.ModifiedOn).ToList() : sponsors.OrderByDescending(c => c.ModifiedOn).ToList();
            }


                //TotalRecords
                var displayResult = sponsors.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
                var totalRecords = sponsors.Count();
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                });
            
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
        [HttpGet]
        // GET: Eventsponsors/Create
        public IActionResult CreateEdit(Int64 id)
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewBag.eid = id;
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return PartialView("CreateEdit");
        }
        [HttpPost]
        public IActionResult CreateEdit(Eventsponsor eventsponsor)
        {
            if (eventsponsor.Id == 0 || eventsponsor.Id == null)
            {
                string mid = cd.HttpContext.Session.GetString("MID");
                var sponsor = new Eventsponsor()
                {
                    EventId = eventsponsor.EventId,
                    SponsorName = eventsponsor.SponsorName,
                    SponsorOrganization = eventsponsor.SponsorOrganization,
                    AmountSponsored = eventsponsor.AmountSponsored,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                };
                _context.Eventsponsors.Add(sponsor);
                _context.SaveChanges();
                return Json("Sponsor is Added...");
            }
            else
            {
                string mid = cd.HttpContext.Session.GetString("MID");
                var sponsor = new Eventsponsor()
                {
                    Id = eventsponsor.Id,
                    EventId = eventsponsor.EventId,
                    SponsorName = eventsponsor.SponsorName,
                    SponsorOrganization = eventsponsor.SponsorOrganization,
                    AmountSponsored = eventsponsor.AmountSponsored,
                    CreatedOn = eventsponsor.CreatedOn,
                    CreatedBy = eventsponsor.CreatedBy,
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                };
                _context.Eventsponsors.Update(sponsor);
                _context.SaveChanges();
                return Json("Sponsor Updated");
            }
           
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
        public IActionResult Edit(int? id)
        {
            var Edit =  _context.Eventsponsors.Find(id);
            return Json(Edit);
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
            var data = _context.Eventsponsors.Where(e => e.Id == id).SingleOrDefault();
            _context.Eventsponsors.Remove(data);
            _context.SaveChanges();
            return Json("success");
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
