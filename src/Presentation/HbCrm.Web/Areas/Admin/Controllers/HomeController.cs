using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HbCrm.Services.Authorize;
using Microsoft.AspNetCore.Authorization;
using HbCrm.Web.Areas.Admin.Models.Admin;
using Newtonsoft.Json;
using HbCrm.Services.Admin;
using HbCrm.Core.Domain.Admin;

namespace HbCrm.Web.Areas.Admin.Controllers
{
    [AdminAuthorize(Policy ="")]
    public class HomeController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }
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
        [AllowAnonymous]
        public IActionResult Login(AdminLoginModel adminLoginModel)
        {
            HbCrm.Core.Domain.Admin.Admin admin= _adminService.GetAdmin(adminLoginModel.UserName);
            AdminLoginSuccessModel loginSuccessModel = new AdminLoginSuccessModel();
            loginSuccessModel.LoginStatus = LoginStatus.Error;
            if (admin.Password == adminLoginModel.Password)
            {
                loginSuccessModel.LoginStatus = LoginStatus.Success;
            }
            string responseData = JsonConvert.SerializeObject(loginSuccessModel);
            return new JsonResult(responseData);
        }
    }

   
}