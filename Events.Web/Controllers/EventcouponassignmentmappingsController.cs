﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
namespace Events.Web.Controllers
{
    public class EventcouponassignmentmappingsController : Controller
    {
        private readonly EventDbContext _context;

        public EventcouponassignmentmappingsController(EventDbContext context)
        {
            _context = context;
        }

        // GET: Eventcouponassignmentmappings
        public async Task<IActionResult> Index(Int64 Id)
        {
            if (Id == null || Id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.VBFriend = _context.Events.Where(e => e.Id == Id).FirstOrDefault();
                ViewData["Eventcoupontypes"] = new SelectList(_context.Eventcoupontypes.Where(c => c.EventId==Id), "Id", "CouponName");
                ViewBag.Eid = Id;
                return View();
            }

        }
        public ActionResult getmembers()
        {
            return Json(_context.Executivemembers.ToList());
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
                Ctype = _context.Eventcouponassignmentmappings.Where(m => m.CouponTypeId == Id);
            }
            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                Ctype = Ctype.Where(x => x.Id.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponTypeId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CouponNumber.ToString().Contains(param.sSearch.ToLower())
                                              || x.Booked.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.Id).ToList() : Ctype.OrderByDescending(c => c.Id).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponTypeId).ToList() : Ctype.OrderByDescending(c => c.CouponTypeId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                Ctype = param.sSortDir_0 == "asc" ? Ctype.OrderBy(c => c.CouponNumber).ToList() : Ctype.OrderByDescending(c => c.CouponNumber).ToList();
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
        //        .Include(e => e.ExecutiveMemberNavigation)
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
        //    ViewData["ExecutiveMember"] = new SelectList(_context.Executivemembers, "Id", "Id");
        //    return View();
        //}

        //// POST: Eventcouponassignmentmappings/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CouponTypeId,CouponNumber,ExecutiveMember,Attendee,Booked")] Eventcouponassignmentmapping eventcouponassignmentmapping)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(eventcouponassignmentmapping);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventcouponassignmentmapping.CouponTypeId);
        //    ViewData["ExecutiveMember"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignmentmapping.ExecutiveMember);
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
        //    ViewData["ExecutiveMember"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignmentmapping.ExecutiveMember);
        //    return View(eventcouponassignmentmapping);
        //}

        //// POST: Eventcouponassignmentmappings/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,CouponTypeId,CouponNumber,ExecutiveMember,Attendee,Booked")] Eventcouponassignmentmapping eventcouponassignmentmapping)
        //{
        //    if (id != eventcouponassignmentmapping.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(eventcouponassignmentmapping);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventcouponassignmentmappingExists(eventcouponassignmentmapping.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CouponTypeId"] = new SelectList(_context.Eventcoupontypes, "Id", "Id", eventcouponassignmentmapping.CouponTypeId);
        //    ViewData["ExecutiveMember"] = new SelectList(_context.Executivemembers, "Id", "Id", eventcouponassignmentmapping.ExecutiveMember);
        //    return View(eventcouponassignmentmapping);
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
        //        .Include(e => e.ExecutiveMemberNavigation)
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