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
    public class EventsponsorsimagesController : Controller
    {
        private readonly EventDbContext _context;

        public EventsponsorsimagesController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventsponsorsimages
        public IActionResult Index()
        {
            return View();
        }
        


        public ActionResult GetEventsponsorsimages(JqueryDatatableParam param)
        {
            var image = _context.Eventsponsorsimages.ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                image = image.Where(x => x.EventId.ToString().Contains(param.sSearch.ToLower())
                                              || x.Id.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                image = param.sSortDir_0 == "asc" ? image.OrderBy(c => c.EventId).ToList() : image.OrderByDescending(c => c.EventId).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                image = param.sSortDir_0 == "asc" ? image.OrderBy(c => c.Id).ToList() : image.OrderByDescending(c => c.Id).ToList();
            }
  
            //TotalRecords
            var displayResult = image.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = image.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        // GET: Eventsponsorsimages/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Eventsponsorsimages == null)
            {
                return NotFound();
            }

            var eventsponsorsimage = await _context.Eventsponsorsimages
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventsponsorsimage == null)
            {
                return NotFound();
            }

            return View(eventsponsorsimage);
        }

        // GET: Eventsponsorsimages/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            return View();
        }

        // POST: Eventsponsorsimages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Eventsponsorsimage eventsponsorsimage)
        {
           
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(eventsponsorsimage.File.FileName);
                string fileName = eventsponsorsimage.File.FileName;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    eventsponsorsimage.File.CopyTo(stream);
                }
                var member = new Eventsponsorsimage()
                {
                    EventId = eventsponsorsimage.EventId,
                    SponsorImage= fileNameWithPath
                };
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsorsimage.EventId);
            //return View(eventsponsorsimage);
        }

        // GET: Eventsponsorsimages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventsponsorsimages == null)
            {
                return NotFound();
            }

            var eventsponsorsimage = await _context.Eventsponsorsimages.FindAsync(id);
            if (eventsponsorsimage == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsorsimage.EventId);
            return View(eventsponsorsimage);
        }

        // POST: Eventsponsorsimages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EventId,SponsorImage")] Eventsponsorsimage eventsponsorsimage)
        {
            if (id != eventsponsorsimage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsponsorsimage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsponsorsimageExists(eventsponsorsimage.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsorsimage.EventId);
            return View(eventsponsorsimage);
        }

        // GET: Eventsponsorsimages/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Eventsponsorsimages == null)
            {
                return NotFound();
            }

            var eventsponsorsimage = await _context.Eventsponsorsimages
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventsponsorsimage == null)
            {
                return NotFound();
            }

            return View(eventsponsorsimage);
        }

        // POST: Eventsponsorsimages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventsponsorsimages == null)
            {
                return Problem("Entity set 'EventDbContext.Eventsponsorsimages'  is null.");
            }
            var eventsponsorsimage = await _context.Eventsponsorsimages.FindAsync(id);
            if (eventsponsorsimage != null)
            {
                _context.Eventsponsorsimages.Remove(eventsponsorsimage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsponsorsimageExists(long id)
        {
          return _context.Eventsponsorsimages.Any(e => e.Id == id);
        }
    }
}
