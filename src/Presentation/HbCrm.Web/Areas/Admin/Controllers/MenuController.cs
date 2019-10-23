using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HbCrm.Core;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Core.Domain.DataEnumerate;
using HbCrm.Services.Authorize;
using HbCrm.Services.Web;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Authorize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class MenuController : AdminBaseController
    {
        private readonly IMenuService _menuService;
        private readonly IWorkContext _context;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService,
            IWorkContext context,
            IMapper mapper)
        {
            _menuService = menuService;
            _context = context;
            _mapper = mapper;
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult Index()
        {
            return View();
        }

        #region 
        //[AdminAuthorize(Policy = PermissionKeys.MenuView)]
        //public IActionResult List( [FromBody] MenuQueryParamInputModel param)
        //{
        //    IPagedList<SysMenu> menus = null;
        //    var result = new PagedListReponseOutPut<SysMenu>();
        //    try
        //    {
        //        menus = _menuService.GetMenus(
        //            pageNumber: param.PageNumber,
        //            pageSize: param.PageSize,
        //            menuName: param.MenuName,
        //            menuSystermName: param.MenuSystermName,
        //            sortName: param.SortName,
        //            sortOrder: param.SortOrder);
        //        result.Rows = menus;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = ReutnStatus.Error;
        //        result.Message = "Error";
        //    }
        //    return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        //}
        #endregion 


        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult List()
        {
           List<SysMenu> menus = null;
            var result = new ListReponseOutPut<SysMenu>();
            try
            {
                menus = _menuService.GetAllMenus();
                result.Rows = menus;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult GetLevelMenus(int parentId)
        {

            var result = new ListReponseOutPut<SysMenu>();
            if (parentId >= 0)
            {
                try
                {
                    var menus = _menuService.GetLevelMenus(parentId: parentId);
                    result.Rows = menus;
                }
                catch (Exception ex)
                {
                    result.Status = ReutnStatus.Error;
                    result.Message = "Error";
                }
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }


        [AdminAuthorize(Policy = PermissionKeys.MenuAdd)]
        [HttpGet]
        public IActionResult Add()
        {            
            return View();
        }


        [AdminAuthorize(Policy = PermissionKeys.MenuAdd)]
        [HttpPost]
        public IActionResult Add( MenuInput menuInputModel)
        {

            var response = new ReponseOutPut();
            //校验菜单系统名称是否存在
            var isExistSystermName= _menuService.ExistMenuByMenuSystermName(menuInputModel.MenuSystermName);

            if (isExistSystermName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "menu_exsit_menuSystermName";
                response.Message = "菜单系统名称已存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysMenu sysMenu = _mapper.Map<MenuInput, SysMenu>(menuInputModel);

            sysMenu.CreateBy = _context.Admin.Id;
            sysMenu.CreatebyName = _context.Admin.UserName;
            sysMenu.CreateDate = DateTime.Now;
            sysMenu.LastUpdateBy = _context.Admin.Id;
            sysMenu.LastUpdateByName = _context.Admin.UserName;
            sysMenu.LastUpdateDate = sysMenu.CreateDate;


            if (menuInputModel.FirstParentMenuId <= 0)
            {
                sysMenu.ParentMenuId = 0;
            }
            else
            {
                if (menuInputModel.SecondParentMenuId <= 0)
                {
                    sysMenu.ParentMenuId = menuInputModel.FirstParentMenuId;
                }
                else
                {
                    if (menuInputModel.ThirdParentMenuId <= 0)
                    {
                        sysMenu.ParentMenuId = menuInputModel.SecondParentMenuId;
                    }
                    else
                    {
                        sysMenu.ParentMenuId = menuInputModel.ThirdParentMenuId;
                        sysMenu.MenuType =MenuType.Function;//三级菜单选中，代表是功能
                    }
                }
            }
            var result = _menuService.AddMenu(sysMenu);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "menu_add_error";
                response.Message = "新增菜单失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }



        [AdminAuthorize(Policy = PermissionKeys.MenuAdd)]
        [HttpPost]
        public IActionResult Delete(int [] menuIds)
        {
            var response = new ReponseOutPut();
            var result = _menuService.DeleteMenu(menuIds);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "error";
                response.Message = "删除菜单失败";
            }

            return new JsonResult(JsonConvert.SerializeObject(response));
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuEdit)]
        public IActionResult Edit(int id)
        {
            var model = _menuService.Get(id);
            return View(model);
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuEdit)]
        [HttpPost]
        public IActionResult Edit(MenuInput  param)
        {
            var response = new ReponseOutPut();
            response.Code = "success";
            response.Message = "更新菜单成功";
            if (!ModelState.IsValid)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "param_vaild_error";

                var errorProperty = ModelState.Values.First(m => m.ValidationState == ModelValidationState.Invalid);
                response.Message = errorProperty.Errors.First().ErrorMessage;//验证不通过的 //全局配置一个验证不通过就不在验证了，只存在一个错误信息

                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            //校验菜单系统名称是否存在
            var isExistSystermName = _menuService.ExistMenuByMenuSystermName(param.MenuSystermName,param.Id);

            if (isExistSystermName)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "menu_exsit_menuSystermName";
                response.Message = "菜单系统名称已存在";
                return new JsonResult(JsonConvert.SerializeObject(response));
            }

            SysMenu model = _mapper.Map<MenuInput, SysMenu>(param);

            model.LastUpdateBy = _context.Admin.Id;
            model.LastUpdateByName = _context.Admin.UserName;
            model.LastUpdateDate = DateTime.Now;


            if (param.FirstParentMenuId <= 0)
            {
                model.ParentMenuId = 0;
            }
            else
            {
                if (param.SecondParentMenuId <= 0)
                {
                    model.ParentMenuId = param.FirstParentMenuId;
                }
                else
                {
                    if (param.ThirdParentMenuId <= 0)
                    {
                        model.ParentMenuId = param.SecondParentMenuId;
                    }
                    else
                    {
                        model.ParentMenuId = param.ThirdParentMenuId;
                        model.MenuType = MenuType.Function;//三级菜单选中，代表是功能
                    }
                }
            }

            var result = _menuService.Update(model);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "error";
                response.Message = "更新菜单失败";
            }
            return new JsonResult(JsonConvert.SerializeObject(response));
        }


    }
}
