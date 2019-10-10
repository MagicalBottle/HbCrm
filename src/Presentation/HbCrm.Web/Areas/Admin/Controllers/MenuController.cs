using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Core;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Authorize;
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
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
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
            var result = new PagedListOutPut<SysMenu>();
            try
            {
                menus = _menuService.GetMenus(param.PageNumber, param.PageSize);
                result.Rows = menus;
            }
            catch (Exception ex)
            {
                result.Status = ReutnStatus.Error;
                result.Message = "Error";
            }
            return new JsonResult(JsonConvert.SerializeObject(result, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff" }));
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuAdd)]
        public IActionResult Add()
        {
            return View();
        }
    }
}
