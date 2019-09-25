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
using Microsoft.AspNetCore.Http;

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        private readonly HbCrm.Services.Authentication.IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IAdminService adminService,
            HbCrm.Services.Authentication.IAuthenticationService authenticationService,
            IWorkContext workContext,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _adminService = adminService;
            _authenticationService = authenticationService;
            _workContext = workContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminDashboard)]
        public IActionResult Index()
        {
            var admin = _workContext.Admin ;
            return View(admin);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Count() >0)
            {
                //有cookie 删除
                _authenticationService.SignOut();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AdminLoginModel adminLoginModel)
        {
            HbCrm.Core.Domain.Admin.SysAdmin admin = _adminService.GetAdminByUserNameNoLazy(adminLoginModel.UserName);
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

        [AllowAnonymous]
        public IActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToAction("Login", "Home",new { Area = "Admin" });
        }

        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }
    }


}