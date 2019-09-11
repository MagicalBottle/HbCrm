using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HbCrm.Services.Authorize;
using Microsoft.AspNetCore.Authorization;
using HbCrm.Web.Areas.Admin.Models.Admin;

namespace HbCrm.Web.Areas.Admin.Controllers
{
    [AdminAuthorize(Policy ="")]
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLoginModel adminLoginModel)
        {
            return View();
        }
    }

   
}