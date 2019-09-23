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
using System.Security.Claims;
using HbCrm.Services.Authentication;
using Microsoft.AspNetCore.Authentication;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Web;

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        private readonly IWorkContext _workContext;
        private readonly HbCrm.Services.Authentication.IAuthenticationService _authenticationService;
        public HomeController(IAdminService adminService,
            HbCrm.Services.Authentication.IAuthenticationService authenticationService,
            IWorkContext workContext
            )
        {
            _adminService = adminService;
            _authenticationService = authenticationService;
            _workContext = workContext;
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminDashboard)]
        public IActionResult Index()
        {
            var admin = _workContext.Admin ;
            List<SysAdminRole> ars = admin.AdminRoles;
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
            HbCrm.Core.Domain.Admin.SysAdmin admin = _adminService.GetAdminByUserName(adminLoginModel.UserName);
            AdminLoginSuccessModel loginSuccessModel = new AdminLoginSuccessModel();
            loginSuccessModel.LoginStatus = LoginStatus.Error;
            if (admin != null && admin.Password == adminLoginModel.Password)
            {
                var r = HttpContext.Request;
                _authenticationService.SignIn(admin, adminLoginModel.IsPersistent);
                loginSuccessModel.LoginStatus = LoginStatus.Success;
                loginSuccessModel.ReturnUrl = adminLoginModel.ReturnUrl;
            }
            string responseData = JsonConvert.SerializeObject(loginSuccessModel);
            return new JsonResult(responseData);
        }
        
        public IActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login", "Home",new { Area = "Admin" });
        }
    }


}