using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Variables

        internal readonly IViewRenderingService _renderingService;
        private readonly ILogger<AccountController> _logger;
        internal readonly IActionContextAccessor _actionContextAccessor;

        #endregion

        #region Constructors

        public BaseController(IViewRenderingService viewRenderingService, ILogger<AccountController> logger, IActionContextAccessor actionContextAccessor)
        {
            _renderingService = viewRenderingService;
            _logger = logger;
        }

        #endregion

    }
}

