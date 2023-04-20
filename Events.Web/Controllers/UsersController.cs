using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using Events.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly EventDbContext _db;
        public UsersController(ILogger<UsersController> logger, EventDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateMember()
        {
            return PartialView("_addECMember");
        }
        public IActionResult CreateMembers(User executivemember)
        {
            if (executivemember.Id == null|| executivemember.Id==0)
            {
                var id = _db.Users.ToList();
                var member = new User()
                {
                    FullName = executivemember.FullName,
                    Designation = executivemember.Designation,
                    AppointedOn = executivemember.AppointedOn,
                    Duties = executivemember.Duties,
                    LoginName = executivemember.LoginName,
                    Password = executivemember.Password,
                    Role = executivemember.Role,
                    Active = executivemember.Active,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };
                _db.Users.Add(member);
                _db.SaveChanges();
                return Json("Member saved.");
            }
            else
            {
                var member = new User()
                {
                    Id = executivemember.Id,
                    FullName = executivemember.FullName,
                    Designation = executivemember.Designation,
                    AppointedOn = executivemember.AppointedOn,
                    Duties = executivemember.Duties,
                    LoginName = executivemember.LoginName,
                    Password = executivemember.Password,
                    Role = executivemember.Role,
                    Active = executivemember.Active,
                    CreatedOn = executivemember.CreatedOn,
                    ModifiedOn = DateTime.Now
                };
                _db.Users.Update(member);
                _db.SaveChanges();
                return Json("Member saved.");
            }
     
        }
        public IActionResult GetEdit(Int64 id)
        {
            var EC = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            return Json(EC);
        }
        public ActionResult GetECMember(JqueryDatatableParam param)
        {
            var eventattendees = _db.Users.ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                eventattendees = eventattendees.Where(x => x.FullName.ToString().Contains(param.sSearch.ToLower())
                                              || x.Designation.ToString().Contains(param.sSearch.ToLower())
                                              || x.Duties.ToString().Contains(param.sSearch.ToLower())
                                              || x.Active.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.FullName).ToList() : eventattendees.OrderByDescending(c => c.FullName).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Designation).ToList() : eventattendees.OrderByDescending(c => c.Designation).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.AppointedOn).ToList() : eventattendees.OrderByDescending(c => c.AppointedOn).ToList();

            }
            else if (param.iSortCol_0 == 3)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Duties).ToList() : eventattendees.OrderByDescending(c => c.Duties).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                eventattendees = param.sSortDir_0 == "asc" ? eventattendees.OrderBy(c => c.Active).ToList() : eventattendees.OrderByDescending(c => c.Active).ToList();
            }

            //TotalRecords
            var displayResult = eventattendees.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = eventattendees.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }
        public IActionResult ECMemberDetails(Int64 id)
        {
            var EC = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_ECDetails", EC);
        }

        public IActionResult DeteleMember(Int64 id)
        {
            var data = _db.Users.Where(e => e.Id == id).SingleOrDefault();
            _db.Users.Remove(data);
            _db.SaveChanges();
            return Json("success");
        }
    }
}
