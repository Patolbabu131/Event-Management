
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using System.Globalization;
using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventcoupontypesController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;
        public EventcoupontypesController(EventDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            cd = httpContextAccessor;
        }

        public async Task<IActionResult> Index(Int64 Id)
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
                Ctype = Ctype.Where(x => x.CouponName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.CouponPrice.ToString().Contains(param.sSearch.ToString())
                                              || x.TotalCoupon.ToString().Contains(param.sSearch.ToString())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponName).ToList() : Ctype.OrderByDescending(c => c.CouponName).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponPrice).ToList() : Ctype.OrderByDescending(c => c.CouponPrice).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Active).ToList() : Ctype.OrderByDescending(c => c.Active).ToList();
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.eid = id;
            ViewData["ModifiedBy"] = new SelectList(_context.Users, "Id", "Id");
            return PartialView("CreateCType");
        }

        [HttpPost]
        public IActionResult CreateCType(Eventcoupontype eventcoupontype)
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            if (eventcoupontype.Id == null || eventcoupontype.Id == 0)
            {

                Eventcoupontype Coupontype = new Eventcoupontype()
                {
                    EventId = eventcoupontype.EventId,
                    CouponName = eventcoupontype.CouponName,
                    CouponPrice = eventcoupontype.CouponPrice,
                    TotalCoupon=eventcoupontype.TotalCoupon,
                    Active = eventcoupontype.Active,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now
                };
                _context.Eventcoupontypes.Add(Coupontype);
                _context.SaveChanges();
                for (var i = 1; i <= eventcoupontype.TotalCoupon; i++)
                {
                    Eventcouponassignmentmapping mapping = new Eventcouponassignmentmapping()
                    {
                        CouponNumber = i,
                        CouponTypeId= Coupontype.Id,
                        EventId = eventcoupontype.EventId,
                    };
                    _context.Eventcouponassignmentmappings.Add(mapping);
                    _context.SaveChanges();
                }
                _context.SaveChanges();
                return Json("Type of Coupon is Created...");
            }
            else
            {
                var Coupontype = _context.Eventcoupontypes.Where(m => m.Id == eventcoupontype.Id).FirstOrDefault();
                    Coupontype.CouponName = eventcoupontype.CouponName;
                    Coupontype.CouponPrice = eventcoupontype.CouponPrice;
                    Coupontype.Active = eventcoupontype.Active;
                    Coupontype.ModifiedBy = Convert.ToInt64(mid);
                    Coupontype.ModifiedOn = DateTime.Now;
                
                _context.Eventcoupontypes.Update(Coupontype);
                _context.SaveChanges();

                return Json("Updated ...");
            }
        }

        public async Task<IActionResult> Edit(long? id)
        {

            var data = _context.Eventcoupontypes.Where(x => x.Id == id).FirstOrDefault();
            return Json(data);
        }

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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", eventcoupontype.CreatedBy);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventcoupontype.EventId);
            ViewData["ModifiedBy"] = new SelectList(_context.Users, "Id", "Id", eventcoupontype.ModifiedBy);
            return View(eventcoupontype);
        }

        public IActionResult Delete(long? id)
        {
            var eventcoupontype = _context.Eventcoupontypes.Where(e => e.Id == id).FirstOrDefault();
           
            var data = _context.Eventcouponassignmentmappings.Where(e => e.CouponTypeId == id && e.Booked == "true").FirstOrDefault();
          
                if (data!=null)
                {
                    return Json("Unable To Delete Coupon. Because " + eventcoupontype.CouponName + " is already assigned to Attendee :" + data.Attendee);
                }
                else
                {
                    _context.Eventcoupontypes.Remove(eventcoupontype);

                    var newdata = _context.Eventcouponassignmentmappings.Where(e=>e.CouponTypeId==id).ToList();
                    foreach(var i in newdata)
                    {
                        _context.Eventcouponassignmentmappings.Remove(i);
                    }
                    _context.SaveChanges();
                    return Json("Coupon Deleted");
                }
          
        }

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Eventcoupontypes == null)
            {
                return Problem("Entity set 'EventDbContext.Eventattendees'  is null.");
            }
            var eventattendee = await _context.Eventattendees.FindAsync(id);
            if (eventattendee != null)
            {
                _context.Eventattendees.Remove(eventattendee);
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
