﻿
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
        private readonly IHttpContextAccessor cd;
        public EventcoupontypesController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd = httpContextAccessor;
        }

        // GET: Eventcoupontypes
        public async Task<IActionResult> Index(Int64 Id)
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

        public ActionResult Getctype(JqueryDatatableParam param, Int64 Id)
        {


            IEnumerable<dynamic> Ctype = null;
            if (Id == null || Id == 0)
            {
                Ctype = _context.Eventcoupontypes.ToList();
            }
            else
            {
                Ctype = _context.Eventcoupontypes.Where(m => m.EventId == Id);
            }
            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                Ctype = Ctype.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
                                              || x.EventId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponName.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponPrice.ToString().Contains(param.sSearch.ToLower())
                                              || x.Active.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreatedOn.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedBy.ToString().Contains(param.sSearch.ToLower())
                                              || x.ModifiedOn.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            else if (param.iSortCol_0 == 0)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Id).ToList() : Ctype.OrderByDescending(c => c.Id).ToList();
            }
            if (param.iSortCol_0 == 1)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.EventId).ToList() : Ctype.OrderByDescending(c => c.EventId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponName).ToList() : Ctype.OrderByDescending(c => c.CouponName).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponPrice).ToList() : Ctype.OrderByDescending(c => c.CouponPrice).ToList();

            }
            else if (param.iSortCol_0 == 4)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Active).ToList() : Ctype.OrderByDescending(c => c.Active).ToList();
            }
            else if (param.iSortCol_0 == 5)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CreatedBy).ToList() : Ctype.OrderByDescending(c => c.CreatedBy).ToList();
            }
            else if (param.iSortCol_0 == 6)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CreatedOn).ToList() : Ctype.OrderByDescending(c => c.CreatedOn).ToList();
            }
            else if (param.iSortCol_0 == 7)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.ModifiedBy).ToList() : Ctype.OrderByDescending(c => c.ModifiedBy).ToList();
            }
            else if (param.iSortCol_0 == 8)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.ModifiedOn).ToList() : Ctype.OrderByDescending(c => c.ModifiedOn).ToList();

            }
            //TotalRecords
            var displayResult = Ctype.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = Ctype.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
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
        [HttpGet]
        public IActionResult CreateCType(Int64 id)
        {
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewBag.eid = id;
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return PartialView("CreateCType");
        }

        [HttpPost]
        public IActionResult CreateCType(Eventcoupontype eventcoupontype)
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            if (eventcoupontype.Id == null || eventcoupontype.Id == 0)
            {

                var Coupontype = new Eventcoupontype()
                {
                    EventId = eventcoupontype.EventId,
                    CouponName = eventcoupontype.CouponName,
                    CouponPrice = eventcoupontype.CouponPrice,
                    Active = eventcoupontype.Active,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,

                };
                _context.Eventcoupontypes.Add(Coupontype);
                _context.SaveChanges();
            }
            else
            {


                var Coupontype = new Eventcoupontype()
                {
                    Id=eventcoupontype.Id,
                    EventId = eventcoupontype.EventId,
                    CouponName = eventcoupontype.CouponName,
                    CouponPrice = eventcoupontype.CouponPrice,
                    Active = eventcoupontype.Active,
                    CreatedOn = eventcoupontype.CreatedOn,
                    CreatedBy = eventcoupontype.CreatedBy,
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,
                };
                _context.Eventcoupontypes.Update(Coupontype);

            }
                _context.SaveChanges();

                return Json("Member saved.");
            
            }

        // GET: Eventcoupontypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {

            var data = _context.Eventcoupontypes.Where(x => x.Id == id).FirstOrDefault();
            return Json(data);
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
            var data = _context.Eventcoupontypes.Where(e => e.Id == id).SingleOrDefault();
            _context.Eventcoupontypes.Remove(data);
            _context.SaveChanges();
            return Json("success");
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
