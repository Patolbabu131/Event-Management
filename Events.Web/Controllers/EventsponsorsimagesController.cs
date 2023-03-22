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

namespace Events.Web.Controllers
{
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
        public IActionResult Create(Int64 Id, Int64 EventId,IFormFile File)
        {

            if (Id == null || Id == 0)
            {
                string path1 = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\Files\\" + File.FileName);

                if (System.IO.File.Exists(path1))
                {
                    return Json("Selected Image is already exists");
                }
                string path = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\Files\\");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                //get file extension
                FileInfo fileInfo = new FileInfo(File.FileName);
                string fileName = File.FileName;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                var member = new Eventsponsorsimage()
                {
                    EventId = EventId,
                    SponsorImage = Path.Combine("\\Files", fileName)
                };
                _context.Add(member);
                _context.SaveChangesAsync();
                return Json("Image Saved");
            }
            else
            {
                var img = _context.Eventsponsorsimages.Where(m => m.Id == Id).FirstOrDefault();
                string i = img.SponsorImage;
                string path111 = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\" + i);

                string path1 = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\Files\\" + File.FileName);

                if (System.IO.File.Exists(path1))
                {
                    return Json("Selected Image is already exists");
                }

                string path2 = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\Files\\");

                //create folder if not exist
                if (!Directory.Exists(path2))
                    Directory.CreateDirectory(path2);

                //get file extension
                FileInfo fileInfo = new FileInfo(File.FileName);
                string fileName = File.FileName;

                string fileNameWithPath = Path.Combine(path2, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }

                img.EventId = EventId;
                img.SponsorImage = Path.Combine("\\Files", fileName);

                _context.Eventsponsorsimages.Update(img);
                _context.SaveChanges();


                if (System.IO.File.Exists(path111))
                {
                    System.IO.File.Delete(path111);
                }
                else
                {
                    return Json("unexpected error..");
                }
                return Json("Image Updated..");
            }

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

            string path = Path.Combine("C:\\Users\\admin\\source\\repos\\EventsPSV\\Events.Web\\wwwroot\\" + i);

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
