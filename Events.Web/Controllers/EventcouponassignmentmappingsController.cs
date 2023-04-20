using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using Org.BouncyCastle.Crypto;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventcouponassignmentmappingsController : Controller
    {
        private readonly EventDbContext _context;

        public EventcouponassignmentmappingsController(EventDbContext context)
        {
            _context = context;
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
                ViewData["Eventcoupontypes"] = new SelectList(_context.Eventcoupontypes.Where(c => c.EventId == Id), "Id", "CouponName");
                ViewData["Users"] = new SelectList(_context.Users.Where(e => e.Role == "Executivemember" && e.Active==true), "Id", "FullName");
                ViewBag.Userss = _context.Users.Where(e => e.Role == "Executivemember" && e.Active == true).Select(s => new { s.Id, s.FullName }).ToList();
                ViewData["User"] = new SelectList(_context.Users.Where(e => e.Role == "Executivemember" && e.Active == true), "Id", "FullName");
                ViewBag.Eid = Id;
                return View();
            }
        }

        public ActionResult getmembers()
        {
            var member = _context.Users.Where(e => e.Role == "Executivemember" && e.Active == true).ToList();
            return Json(member);
        }

        public ActionResult Getctype(JqueryDatatableParam param, Int64 Id)
        {
            IEnumerable<dynamic> Ctype = null;
            if (Id == null || Id == 0)
            {
                Ctype = _context.Eventcouponassignmentmappings.ToList();
            }
            else
                {


                Ctype = (from a in _context.Eventcouponassignmentmappings
                         let attendeename = _context.Eventattendees.Where(c => a.Attendee.HasValue && c.Id == a.Attendee.Value).Select(s => s.AttendeeName).FirstOrDefault()
                         where a.CouponTypeId == Id
                         select new
                         {
                             Id=a.Id,
                             CouponNumber = a.CouponNumber,
                             User = a.User,
                             Booked = a.Booked,
                             Attendee = !string.IsNullOrEmpty(attendeename) ? attendeename : string.Empty
                         }).ToList();
            }
            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                Ctype = Ctype.Where(x => x.CouponNumber.ToString().Contains(param.sSearch.ToString())
                                              || x.User.ToString().ToLower().Contains(param.sSearch.ToLower())
                                              || x.Attendee.ToString().ToLower().Contains(param.sSearch.ToLower())
                                              || x.Booked.ToString().ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponNumber).ToList() : Ctype.OrderByDescending(c => c.CouponNumber).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Attendee).ToList() : Ctype.OrderByDescending(c => c.Attendee).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Booked).ToList() : Ctype.OrderByDescending(c => c.Booked).ToList();
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


        //// GET: Eventcouponassignmentmappings/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null || _context.Eventcouponassignmentmappings == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventcouponassignmentmapping = await _context.Eventcouponassignmentmappings
        //        .Include(e => e.CouponType)
        //        .Include(e => e.UserNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (eventcouponassignmentmapping == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(eventcouponassignmentmapping);
        //}

        //// GET: Eventcouponassignmentmappings/Create
        //public IActionResult Create()
        //{
        //    ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id");
        //    ViewData["User"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: Eventcouponassignmentmappings/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CouponTypeId,CouponNumber,User,Attendee,Booked")] Eventcouponassignmentmapping eventcouponassignmentmapping)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(eventcouponassignmentmapping);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventcouponassignmentmapping.CouponTypeId);
        //    ViewData["User"] = new SelectList(_context.Users, "Id", "Id", eventcouponassignmentmapping.User);
        //    return View(eventcouponassignmentmapping);
        //}

        //// GET: Eventcouponassignmentmappings/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null || _context.Eventcouponassignmentmappings == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventcouponassignmentmapping = await _context.Eventcouponassignmentmappings.FindAsync(id);
        //    if (eventcouponassignmentmapping == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventcouponassignmentmapping.CouponTypeId);
        //    ViewData["User"] = new SelectList(_context.Users, "Id", "Id", eventcouponassignmentmapping.User);
        //    return View(eventcouponassignmentmapping);
        //}

        // POST: Eventcouponassignmentmappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public async Task<IActionResult> Edit(Int64 Id, Eventcouponassignmentmapping eventcouponassignmentmapping)
        {

            var mapping = _context.Eventcouponassignmentmappings.Where(e => e.Id == eventcouponassignmentmapping.Id).FirstOrDefault();
            mapping.User = eventcouponassignmentmapping.User;
            _context.Eventcouponassignmentmappings.Update(mapping);
            await _context.SaveChangesAsync();

            return Json(mapping.EventId);
        }
        public async Task<IActionResult> Edit2(Int64 User, string strids)
        {
            if (strids == null)
            {
                return Json("Select Coupon");
            }
            var ids = strids.Split(",").Select(long.Parse).ToList();
            var mapping = _context.Eventcouponassignmentmappings.Where(e => ids.Contains(e.Id)).ToList();
            if (mapping.Count() > 0)
            {
                foreach (var item in mapping)
                {
                    item.User = User;
                    _context.Eventcouponassignmentmappings.Update(item);
                    await _context.SaveChangesAsync();
                }
            }

            return Json("ok");
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Int64 Id, Eventcouponassignmentmapping eventcouponassignmentmapping)
        //{

        //    var mapping = _context.Eventcouponassignmentmappings.Where(e => e.Id == eventcouponassignmentmapping.Id).FirstOrDefault();
        //    mapping.User = eventcouponassignmentmapping.User;
        //    _context.Eventcouponassignmentmappings.Update(mapping);
        //    await _context.SaveChangesAsync();

        //    return Json();
        //}

        //// GET: Eventcouponassignmentmappings/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null || _context.Eventcouponassignmentmappings == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventcouponassignmentmapping = await _context.Eventcouponassignmentmappings
        //        .Include(e => e.CouponType)
        //        .Include(e => e.UserNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (eventcouponassignmentmapping == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(eventcouponassignmentmapping);
        //}

        //// POST: Eventcouponassignmentmappings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    if (_context.Eventcouponassignmentmappings == null)
        //    {
        //        return Problem("Entity set 'EventDbContext.Eventcouponassignmentmappings'  is null.");
        //    }
        //    var eventcouponassignmentmapping = await _context.Eventcouponassignmentmappings.FindAsync(id);
        //    if (eventcouponassignmentmapping != null)
        //    {
        //        _context.Eventcouponassignmentmappings.Remove(eventcouponassignmentmapping);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EventcouponassignmentmappingExists(long id)
        {
            return (_context.Eventcouponassignmentmappings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
