using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Core;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Admin;
using HbCrm.Services.Authorize;
using HbCrm.Services.Web;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        private readonly IWorkContext _context;
        public AccountController(IAdminService adminService, IWorkContext context)
        {
            _adminService = adminService;
            _context = context;
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminView)]
        public IActionResult Index()
        {
            return View();
        }
        
        [AdminAuthorize(Policy = PermissionKeys.AdminView)]
        public IActionResult List( AdminQueryParamInputModel param)
        {
            IPagedList<SysAdmin> admin = null;
            var result = new PagedListReponseOutPut<SysAdmin>();
            try
            {
                admin = _adminService.GetAdmins(
                    pageNumber: param.PageNumber,
                    pageSize: param.PageSize,
                    sortName: param.SortName,
                    sortOrder: param.SortOrder,
                    userName: param.UserName,
                    nickName: param.NickName,
                    email:param.Email,
                    mobilePhone:param.MobilePhone,
                    qQ:param.QQ,
                    weChar:param.WeChar);

                result.Rows = admin;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }
    }
}
