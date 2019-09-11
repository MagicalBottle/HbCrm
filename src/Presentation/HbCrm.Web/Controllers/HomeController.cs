using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HbCrm.Services.Admin;

namespace HbCrm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminService _adminService;
        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
           var admin= _adminService.GetAdmin("lily");
            return View();
        }
    }
}