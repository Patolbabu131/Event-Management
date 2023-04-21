
using Events.Web.Models;
using MySqlX.XDevAPI;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;

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
        public async Task<IActionResult> Login(string email_username, string  password)
        {
            var data = _db.Users.Where(e=>e.LoginName==email_username).SingleOrDefault();
            if (data != null)
            {
                bool isValid = (data.LoginName == email_username && DecryptPassword(data.Password) == password && data.Active==true);
                if (isValid)

                {
                    string myString = data.Id.ToString();

                    cd.HttpContext.Session.SetString("MID",myString);

                    //Authorization

                    
                    List<Claim> claims = new List<Claim>(){
                        new Claim(ClaimTypes.SerialNumber, myString),
                        new Claim(ClaimTypes.Role,data.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToAction("Index", "Events");
                }
                else
                {
                    ViewBag.Message = "UserName or password is wrong";
                    return View();
                }
                return View();
            }
               return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            cd.HttpContext.Session.SetString("MID", " ");
            return RedirectToAction("Login");
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
                string encryptedPassword=Convert.ToBase64String(storePassword);
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

