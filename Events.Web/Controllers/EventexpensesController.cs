using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventexpensesController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IHttpContextAccessor cd;
        public EventexpensesController(EventDbContext context, IHttpContextAccessor cd)
        {
            _context = context;
            this.cd = cd;   
        }

        // GET: Eventexpenses
        public async Task<IActionResult> Index(Int64 Id)
        {
            if (Id == null || Id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.VBFriend = _context.Events.Where(e=>e.Id==Id).FirstOrDefault();
                ViewBag.Eid = Id;
                return View();
            }

            

        }
        public ActionResult GetEventwxpenses(JqueryDatatableParam param, Int64 Id)
        {


            IEnumerable<dynamic> expenses = null;
            if (Id == null || Id == 0)
            {
                expenses = _context.Eventexpenses.ToList();
            }
            else
            {
                expenses = _context.Eventexpenses.Where(m => m.EventId == Id);
            }
            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                expenses = expenses.Where(x => x.ExpenseName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.ExpenseSubject.ToLower().Contains(param.sSearch.ToLower())
                                              || x.AmountSpent.ToString().Contains(param.sSearch.ToString())
                                              || x.Remarks.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                expenses = param.sSortDir_0 == "asc" ? expenses.OrderBy(c => c.ExpenseName).ToList() : expenses.OrderByDescending(c => c.ExpenseName).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                expenses = param.sSortDir_0 == "asc" ? expenses.OrderBy(c => c.ExpenseSubject).ToList() : expenses.OrderByDescending(c => c.ExpenseSubject).ToList();

            }
            else if (param.iSortCol_0 == 2)
            {
                expenses = param.sSortDir_0 == "asc" ? expenses.OrderBy(c => c.AmountSpent).ToList() : expenses.OrderByDescending(c => c.AmountSpent).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                expenses = param.sSortDir_0 == "asc" ? expenses.OrderBy(c => c.Remarks).ToList() : expenses.OrderByDescending(c => c.Remarks).ToList();
            }
            
            //TotalRecords
            var displayResult = expenses.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = expenses.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
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

        [HttpGet]
        public IActionResult CreateEdit(Int64 Id)
        {
            if (cd.HttpContext.Session.GetString("MID") == "0") {
                return RedirectToAction("login","Account");
            }
            ViewData["CreatedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            ViewBag.eid = Id;
            ViewData["ModifiedBy"] = new SelectList(_context.Executivemembers, "Id", "Id");
            return PartialView("CreateEdit");
        }

        [HttpPost]
        public  IActionResult CreateEdit( Eventexpense eventexpense)
        {
            string mid = cd.HttpContext.Session.GetString("MID");
            if (eventexpense.Id == null || eventexpense.Id == 0)
            {

                var Expenses = new Eventexpense()
                {
                    EventId = eventexpense.EventId,
                    ExpenseName = eventexpense.ExpenseName,
                    ExpenseSubject = eventexpense.ExpenseSubject,
                    AmountSpent = eventexpense.AmountSpent,
                    Remarks = eventexpense.Remarks,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Convert.ToInt64(mid),
                    ModifiedBy = Convert.ToInt64(mid),
                    ModifiedOn = DateTime.Now,

                };
                _context.Eventexpenses.Add(Expenses);
                _context.SaveChanges();

                return Json("Expense Added....");
            }
            else
            {

                var Expenses = _context.Eventexpenses.Where(m => m.Id == eventexpense.Id).FirstOrDefault();

                Expenses.ExpenseName = eventexpense.ExpenseName;
                Expenses.ExpenseSubject = eventexpense.ExpenseSubject;
                Expenses.AmountSpent = eventexpense.AmountSpent;
                Expenses.Remarks = eventexpense.Remarks;
                Expenses.ModifiedBy = Convert.ToInt64(mid);
                Expenses.ModifiedOn = DateTime.Now;

               
                _context.Eventexpenses.Update(Expenses);
                _context.SaveChanges();

                return Json("Expense Updated...");
            }
      

        }

        // GET: Eventexpenses/Edit/5
        public async Task<IActionResult> Edit(Int64 id)
        {
        

            var eventexpense = await _context.Eventexpenses.FindAsync(id);
            
            return Json(eventexpense);
        }

        // GET: Eventexpenses/Delete/5
        public IActionResult Delete(long? id)
        {
            var data = _context.Eventexpenses.Where(e => e.Id == id).FirstOrDefault();
            _context.Eventexpenses.Remove(data);
            _context.SaveChanges();
            return Json("success");
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
