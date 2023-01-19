using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class ExecutiveMembersController : Controller
    {
        private readonly ILogger<ExecutiveMembersController> _logger;

        public ExecutiveMembersController(ILogger<ExecutiveMembersController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

