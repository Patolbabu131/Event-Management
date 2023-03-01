using Events.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;
using Events.Database;
using Microsoft.EntityFrameworkCore.Migrations;
using Events.Web.eventcontext;
using System.Diagnostics.Metrics;

namespace Events.Web.Controllers
{
    public class EventsponsorsimagesController1 : Controller
    {
        private readonly EventDbContext _context;
        public EventsponsorsimagesController1(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventsponsorsimages
        public async Task<IActionResult> Index()
        {
            var eventDbContext = _context.Eventsponsorsimages.Include(e => e.Event);
            return View(await eventDbContext.ToListAsync());
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
        public IActionResult Create(Eventsponsorsimage eventsponsorsimage)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(eventsponsorsimage.File.FileName);
            string fileName = eventsponsorsimage.File.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                eventsponsorsimage.File.CopyTo(stream);
            }

        //    var img = new Eventsponsorsimage()
        //    {
        //        img.EventId = eventsponsorsimage.EventId;
        //    };
        //_con
        //_db.Executivemembers.Add(img);
        //        _db.SaveChanges();
            return View(eventsponsorsimage);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,EventId,SponsorImage,Image")] Eventsponsorsimage eventsponsorsimage)
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
