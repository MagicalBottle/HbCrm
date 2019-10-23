using HbCrm.Web.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HbCrm.Web.Areas.Admin.Models.Authorize;

namespace HbCrm.Web.Areas.Validators.Admin
{
    public class MenuInputValidator : HbCrmAdminBaseValidator<MenuInput>
    {
        public MenuInputValidator()
        {
            RuleFor(c => c.MenuName).NotEmpty().WithMessage("菜单名称不能为空");
            RuleFor(c => c.MenuSystermName).NotEmpty().WithMessage("菜单系统名称不能为空");
            RuleFor(c => c.MenuIcon).NotEmpty().WithMessage("菜单图标不能为空");
            RuleFor(c => c.MenuUrl).NotEmpty().WithMessage("菜单地址不能为空");
            //RuleFor(c => c.MenuSort).NotEmpty().WithMessage("菜单排序不能为空");
            RuleFor(c => c.MenuType).NotEmpty().WithMessage("菜单类型不能为空");
        }
    }
}
