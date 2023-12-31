﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Framework.Impl.Frontside;
using Events.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly EventDbContext _db;
        private readonly IHttpContextAccessor cd;
        public UsersController(ILogger<UsersController> logger, EventDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _db = db;
            cd = httpContextAccessor;
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
            string mid = cd.HttpContext.Session.GetString("MID");



            if (executivemember.Id == null || executivemember.Id == 0)
            {
                var user = _db.Users.Where(e => e.LoginName == executivemember.LoginName).FirstOrDefault();
                if (user==null)
                {
                    var id = _db.Users.ToList();
                    var member = new User()
                    {
                        FullName = executivemember.FullName,
                        Designation = executivemember.Designation,
                        AppointedOn = executivemember.AppointedOn,
                        Duties = executivemember.Duties,
                        LoginName = executivemember.LoginName,
                        Password = EncryptPassword(executivemember.Password),
                        Role = executivemember.Role,
                        Active = executivemember.Active,
                        CreatedBy = Convert.ToInt64(mid),
                        ModifiedBy = Convert.ToInt64(mid),
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };
                    _db.Users.Add(member);
                    _db.SaveChanges();
                    return Json("Member saved.");
                }
                return Json("true");
            }
            else
            {
                var user = _db.Users.Where(e => e.LoginName == executivemember.LoginName && e.Id != executivemember.Id).FirstOrDefault();
                if (user == null)
                {
                    var member = new User()
                    {
                        Id = executivemember.Id,
                        FullName = executivemember.FullName,
                        Designation = executivemember.Designation,
                        AppointedOn = executivemember.AppointedOn,
                        Duties = executivemember.Duties,
                        LoginName = executivemember.LoginName,
                        Password = EncryptPassword(executivemember.Password),
                        Role = executivemember.Role,
                        Active = executivemember.Active,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = Convert.ToInt64(mid)
                };
                    _db.Users.Update(member);
                    _db.SaveChanges();
                    return Json("Member saved.");
                }
                return Json("true");
            }



        }
        public IActionResult GetEdit(Int64 id)
        {
            //var EC = (from a in _db.Users.Where(a=>a.Id) 
            //                      select new
            //                      {
            //                          Id= a.Id,
            //                          FullName = a.FullName,
            //                          Designation = a.Designation,
            //                          Duties = a.Duties,
            //                          Password = a.Password,
            //                          AppointedOn = a.AppointedOn,
            //                          Active = a.Active,
            //                          LoginName = a.LoginName,
            //                          Role = a.Role,
            //                      }).FirstOrDefault();

            var EC = _db.Users.Where(e => e.Id == id).FirstOrDefault();
            EC.Password = DecryptPassword(EC.Password);
            return Json(EC);
        }
        public ActionResult GetECMember(JqueryDatatableParam param)
        {
            var eventattendees = (from a in _db.Users

                                  select new
                                  {
                                      id = a.Id,
                                      FullName=a.FullName,
                                      Designation = a.Designation,
                                      Duties=a.Duties,
                                      AppointedOn=a.AppointedOn,
                                      Active=a.Active,
                                      LoginName = a.LoginName,
                                      Role = a.Role,
                                  }).ToList();



            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                eventattendees = eventattendees.Where(x => x.FullName.ToLower().ToString().Contains(param.sSearch.ToLower())
                                              || x.Designation.ToLower().ToString().Contains(param.sSearch.ToLower())
                                              || x.Duties.ToLower().ToString().Contains(param.sSearch.ToLower())).ToList();
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
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }
        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
    }
}
