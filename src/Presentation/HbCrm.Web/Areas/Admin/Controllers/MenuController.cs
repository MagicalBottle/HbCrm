using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Core;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Authorize;
using HbCrm.Services.Web;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Authorize;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class MenuController : AdminBaseController
    {
        private readonly IMenuService _menuService;
        private readonly IWorkContext _context;
        public MenuController(IMenuService menuService,IWorkContext context)
        {
            _menuService = menuService;
            _context = context;
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult List( [FromBody] MenuQueryParamInputModel param)
        {
            IPagedList<SysMenu> menus = null;
            var result = new PagedListReponseOutPut<SysMenu>();
            try
            {
                menus = _menuService.GetMenus(
                    pageNumber: param.PageNumber,
                    pageSize: param.PageSize,
                    menuName: param.MenuName,
                    menuSystermName: param.MenuSystermName,
                    sortName: param.SortName,
                    sortOrder: param.SortOrder);
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
        public IActionResult Add([FromBody] MenuInputModel menuInputModel)
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

            SysMenu sysMenu = new SysMenu();
            sysMenu.MenuName = menuInputModel.MenuName;
            sysMenu.MenuSystermName = menuInputModel.MenuSystermName;
            sysMenu.MenuUrl = menuInputModel.MenuUrl;
            sysMenu.MenuSort = menuInputModel.MenuSort;
            sysMenu.MenuIcon = menuInputModel.MenuIcon;
            sysMenu.MenuRemark = menuInputModel.MenuRemark;

            DateTime dateTimeNow = DateTime.Now;
            sysMenu.CreateBy = _context.Admin.Id;
            sysMenu.CreatebyName = _context.Admin.UserName;
            sysMenu.CreateDate = dateTimeNow;
            sysMenu.LastUpdateBy = _context.Admin.Id;
            sysMenu.LastUpdateByName = _context.Admin.UserName;
            sysMenu.LastUpdateDate = dateTimeNow;


            if (menuInputModel.FirstParentMenuId == 0)
            {
                sysMenu.ParentMenuId = 0;
            }
            else
            {
                if (menuInputModel.SecondParentMenuId == 0)
                {
                    sysMenu.ParentMenuId = menuInputModel.FirstParentMenuId;
                }
                else
                {
                    sysMenu.ParentMenuId = menuInputModel.SecondParentMenuId;
                }
            }
            var result = _menuService.AddMenu(sysMenu);
            if (result < 0)
            {
                response.Status = ReutnStatus.Error;
                response.Code = "menu_add_error";
                response.Message = "新增菜单失败";
            }

            return View();
        }
    }
}
