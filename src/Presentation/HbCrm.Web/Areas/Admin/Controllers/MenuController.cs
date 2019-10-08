using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Authorize;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HbCrm.Web.Areas.Admin.Controllers
{
    public class MenuController : AdminBaseController
    {
        [AdminAuthorize(Policy = PermissionKeys.MenuView)]
        public IActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(Policy = PermissionKeys.MenuAdd)]
        public IActionResult Add()
        {
            return View();
        }
    }
}
