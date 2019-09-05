using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HbCrm.Services.Authorize;

namespace HbCrm.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize(Policy ="")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}