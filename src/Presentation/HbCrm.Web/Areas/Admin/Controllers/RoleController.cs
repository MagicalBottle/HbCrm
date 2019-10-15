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
        public IActionResult GetAllRoles()
        {
            List<SysRole> roles = null;
            var result = new ListReponseOutPut<RoleSelectOutPut>();
            try
            {
                roles = _roleService.GetAllRoles();

                List<RoleSelectOutPut> rows = _mapper.Map<List<SysRole>, List<RoleSelectOutPut>>(roles);
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
