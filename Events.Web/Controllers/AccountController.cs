
using Events.Web.Models;
using MySqlX.XDevAPI;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly EventDbContext _db;
        private readonly IHttpContextAccessor cd;
        public AccountController(ILogger<AccountController> logger, EventDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _db = db;
            cd = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(string email_username, Int64 password)
        {
            var data = _db.Executivemembers.Where(r => r.Id == password && r.FullName == email_username).FirstOrDefault();
            if(data == null)
            {
                ViewBag.Message = "UserName or password is wrong";
                return View();
            }
            else
            {
                string myString = data.Id.ToString();
                cd.HttpContext.Session.SetString("MID", myString);
                return RedirectToAction("Index", "Events");
            }
        }
    }
}

