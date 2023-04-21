    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using System.IO;
using System.Diagnostics.Metrics;
using System.Drawing.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Net.WebRequestMethods;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventsponsorsimagesController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;


        public EventsponsorsimagesController(EventDbContext context, IHttpContextAccessor cd)
        {
            _context = context;
            this.cd = cd;
        }

        // GET: Eventsponsorsimages
        public IActionResult Index(Int64 Id)
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



        public ActionResult GetEventsponsorsimages(JqueryDatatableParam param, Int64 Id)
        {
            IEnumerable<dynamic> image = null;
            if (Id == null || Id == 0)
            {
                image = _context.Eventsponsorsimages.ToList();
            }
            else
            {
                image = _context.Eventsponsorsimages.Where(m => m.EventId == Id);
            }
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
        [HttpGet]
        public IActionResult Create(Int64 id)
        {
            ViewBag.Eid = id;
            return PartialView("Create");
        }

        // POST: Eventsponsorsimages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create( Int64 EventId,IFormFile File)
        {

                string CurrentDirectory = System.Environment.CurrentDirectory;
                FileInfo fileInfo = new FileInfo(File.FileName);
                string Name = File.FileName;

                var fileName = EventId.ToString() + Name;

                var sponimg = _context.Eventsponsorsimages.Where(e=>e.EventId==EventId).ToList();

                foreach (var i in sponimg)
                {
                    if (Equals(i.SponsorImage, fileName))
                    {
                        return Json("Selected Image is already exists");
                    }
                }

               string path = Path.Combine(CurrentDirectory+ "\\wwwroot\\Files");            
               string fileNameWithPath = Path.Combine(path, fileName);

               using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
               {
                    File.CopyTo(stream);
               }
               var member = new Eventsponsorsimage()
               {
                    EventId = EventId,
                    SponsorImage = fileName
               };
               _context.Add(member);
               _context.SaveChangesAsync();
            return Json("Image Saved");
        }

         

        // GET: Eventsponsorsimages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Eventsponsorsimages == null)
            {
                return NotFound();
            }

            var eventsponsorsimage = await _context.Eventsponsorsimages.FindAsync(id);
            ViewBag.img = eventsponsorsimage.SponsorImage;
            if (eventsponsorsimage == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventsponsorsimage.EventId);
            return Json(eventsponsorsimage);
        }

        public IActionResult Edit1()
        {
            return PartialView("Edit");
        }
        // POST: Eventsponsorsimages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Eventsponsorsimage eventsponsorsimage)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsponsorsimage);
                    _context.SaveChangesAsync();
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
        public IActionResult Delete(long? id)
        {
            if (id == null || _context.Eventsponsorsimages == null)
            {
                return NotFound();
            }

            var image = _context.Eventsponsorsimages.Where(m => m.Id == id).FirstOrDefault();
            string i = image.SponsorImage;
            string CurrentDirectory = System.Environment.CurrentDirectory;
            string path = Path.Combine(CurrentDirectory + "\\wwwroot\\" + i);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Eventsponsorsimages.Remove(image);
            _context.SaveChanges();


            return Json("success");
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
