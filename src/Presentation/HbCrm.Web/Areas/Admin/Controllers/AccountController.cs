using AutoMapper;
using HbCrm.Core;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Data;
using HbCrm.Services.Admin;
using HbCrm.Services.Authorize;
using HbCrm.Services.Web;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        private readonly IAdminService _adminService;
        private readonly IWorkContext _context;
        private readonly IMapper _mapper;
        public AccountController(IAdminService adminService,
            IWorkContext context,
           IMapper mapper)
        {
            _adminService = adminService;
            _context = context;
            _mapper = mapper;
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminView)]
        public IActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminView)]
        public IActionResult List(AdminQueryParamInput param)
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
                    email: param.Email,
                    mobilePhone: param.MobilePhone,
                    qQ: param.QQ,
                    weChar: param.WeChar);

                result.Rows = admin;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }


        [AdminAuthorize(Policy = PermissionKeys.AdminAdd)]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminAdd)]
        [HttpPost]
        public IActionResult Add(AdminInput param)
        {
            var response = new ReponseOutPut();
            response.Code = "menu_add_success";
            response.Message = "新增账号成功";
            if (!ModelState.IsValid)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "param_vaild_error";

                var errorProperty = ModelState.Values.First(m => m.ValidationState == ModelValidationState.Invalid);
                response.Message = errorProperty.Errors.First().ErrorMessage;//验证不通过的 //全局配置一个验证不通过就不在验证了，只存在一个错误信息

                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            // 检查用户名是否重复
            var isExistUserName = _adminService.ExistAdminUserName(param.UserName);
            if (isExistUserName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "username_is_exist";
                response.Message = "用户名已经存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysAdmin admin = _mapper.Map<AdminInput, SysAdmin>(param);

            admin.CreateBy = _context.Admin.Id;
            admin.CreatebyName = _context.Admin.UserName;
            admin.CreateDate = DateTime.Now;
            admin.LastUpdateBy = admin.CreateBy;
            admin.LastUpdateByName = admin.CreatebyName;
            admin.LastUpdateDate = admin.CreateDate;

           

            var result = _adminService.AddAdmin(admin,param.RoleIds);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "menu_add_error";
                response.Message = "新增账号失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminEdit)]
        public IActionResult Edit(int id)
        {
            var admin = _adminService.GetAdminById(id);
            return View(admin);
        }

        [AdminAuthorize(Policy = PermissionKeys.AdminEdit)]
        [HttpPost]
        public IActionResult Edit(AdminInput param)
        {
            var response = new ReponseOutPut();
            response.Code = "account_edit_success";
            response.Message = "新增账号成功";
            if (!ModelState.IsValid)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "param_vaild_error";

                var errorProperty = ModelState.Values.First(m => m.ValidationState == ModelValidationState.Invalid);
                response.Message = errorProperty.Errors.First().ErrorMessage;//验证不通过的 //全局配置一个验证不通过就不在验证了，只存在一个错误信息

                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            // 检查用户名是否重复
            var isExistUserName = _adminService.ExistAdminUserName(param.UserName,param.Id);
            if (isExistUserName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "username_is_exist";
                response.Message = "用户名已经存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysAdmin admin = _mapper.Map<AdminInput, SysAdmin>(param);

            admin.LastUpdateBy = _context.Admin.Id;
            admin.LastUpdateByName = _context.Admin.UserName;
            admin.LastUpdateDate = DateTime.Now;

            var result = _adminService.UpdateAdmin(admin, param.RoleIds);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "account_edit_error";
                response.Message = "更新账号失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }              
        
        [AdminAuthorize(Policy = PermissionKeys.AdminView)]
        public IActionResult GetAllAdmins()
        {
            List<SysAdmin> admins = null;
            var result = new ListReponseOutPut<SelectOutPut>();
            try
            {
                admins = _adminService.GetAllAdmins();

                List<SelectOutPut> rows = _mapper.Map<List<SysAdmin>, List<SelectOutPut>>(admins);
                result.Rows = rows;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Code = "get_data_error";
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result));
        }


    }
}
