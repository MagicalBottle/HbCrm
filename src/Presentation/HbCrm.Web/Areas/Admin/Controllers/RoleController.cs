using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HbCrm.Core;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Admin;
using HbCrm.Services.Authorize;
using HbCrm.Services.Web;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class RoleController : AdminBaseController
    {

        private readonly IRoleService _roleService;
        private readonly IWorkContext _context;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService,
            IWorkContext context,
           IMapper mapper)
        {
            _roleService = roleService;
            _context = context;
            _mapper = mapper;
        }

        [AdminAuthorize(Policy = PermissionKeys.RoleView)]
        public IActionResult Index()
        {
            return View();
        }


        [AdminAuthorize(Policy = PermissionKeys.RoleView)]
        public IActionResult List(RoleQueryParamInput param)
        {
            IPagedList<SysRole> role = null;
            var result = new PagedListReponseOutPut<SysRole>();
            try
            {
                role = _roleService.GetRoles(
                    pageNumber: param.PageNumber,
                    pageSize: param.PageSize,
                    sortName: param.SortName,
                    sortOrder: param.SortOrder,
                    roleName: param.RoleName,
                    roleStatus: param.RoleStatus,
                    roleRemark: param.RoleRemark);

                result.Rows = role;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }



        [AdminAuthorize(Policy = PermissionKeys.RoleView)]
        public IActionResult GetAllRoles()
        {
            List<SysRole> roles = null;
            var result = new ListReponseOutPut<SelectOutPut>();
            try
            {
                roles = _roleService.GetAllRoles();

                List<SelectOutPut> rows = _mapper.Map<List<SysRole>, List<SelectOutPut>>(roles);
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

        [AdminAuthorize(Policy = PermissionKeys.RoleAdd)]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [AdminAuthorize(Policy = PermissionKeys.RoleAdd)]
        [HttpPost]
        public IActionResult Add(RoleInput param)
        {
            var response = new ReponseOutPut();
            response.Code = "role_add_success";
            response.Message = "新增角色成功";
            if (!ModelState.IsValid)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "param_vaild_error";

                var errorProperty = ModelState.Values.First(m => m.ValidationState == ModelValidationState.Invalid);
                response.Message = errorProperty.Errors.First().ErrorMessage;//验证不通过的 //全局配置一个验证不通过就不在验证了，只存在一个错误信息

                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            // 检查用户名是否重复
            var isExistUserName = _roleService.ExistRoleName(param.RoleName);
            if (isExistUserName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "rolename_is_exist";
                response.Message = "角色名称已经存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysRole role = _mapper.Map<RoleInput, SysRole>(param);

            role.CreateBy = _context.Admin.Id;
            role.CreatebyName = _context.Admin.UserName;
            role.CreateDate = DateTime.Now;
            role.LastUpdateBy = role.CreateBy;
            role.LastUpdateByName = role.CreatebyName;
            role.LastUpdateDate = role.CreateDate;

            var result = _roleService.AddRole(role, param.AdminIds);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "role_add_error";
                response.Message = "新增角色失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }


        [AdminAuthorize(Policy = PermissionKeys.RoleEdit)]
        public IActionResult Edit(int id)
        {
            var role = _roleService.GetRoleById(id);
            return View(role);
        }

        [AdminAuthorize(Policy = PermissionKeys.RoleEdit)]
        [HttpPost]
        public IActionResult Edit(RoleInput param)
        {
            var response = new ReponseOutPut();
            response.Code = "role_edit_success";
            response.Message = "编辑角色成功";
            if (!ModelState.IsValid)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "param_vaild_error";

                var errorProperty = ModelState.Values.First(m => m.ValidationState == ModelValidationState.Invalid);
                response.Message = errorProperty.Errors.First().ErrorMessage;//验证不通过的 //全局配置一个验证不通过就不在验证了，只存在一个错误信息

                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            // 检查用户名是否重复
            var isExistUserName = _roleService.ExistRoleName(param.RoleName, param.Id);
            if (isExistUserName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "rolename_is_exist";
                response.Message = "角色名称已经存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysRole role = _mapper.Map<RoleInput, SysRole>(param);

            role.LastUpdateBy = _context.Admin.Id;
            role.LastUpdateByName = _context.Admin.UserName;
            role.LastUpdateDate = DateTime.Now;

            var result = _roleService.UpdateRole(role, param.AdminIds);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "role_edit_error";
                response.Message = "编辑角色失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }

        [AdminAuthorize(Policy = PermissionKeys.RoleDelete)]
        public IActionResult Delete(int id)
        {
            var result = _roleService.DeleteRoleById(id);

            var response = new ReponseOutPut();
            response.Code = "role_delete_success";
            response.Message = "角色删除成功";
            if (result < 0)
            {
                response.Code = "role_delete_error";
                response.Message = "角色删除失败";
            }
            return new JsonResult(JsonConvert.SerializeObject(response));
        }

        //[AdminAuthorize(Policy = PermissionKeys.RoleEdit)]
        //public IActionResult Permission(int id)
        //{
        //    var role = _roleService.GetMenus(id);
        //    return View(role);
        //}


        //[AdminAuthorize(Policy = PermissionKeys.RoleEdit)]
        //[HttpPost]
        //public IActionResult Permission(int id)
        //{
        //    var role = _roleService.GetMenus(id);
        //    return View(role);
        //}
    }
}
