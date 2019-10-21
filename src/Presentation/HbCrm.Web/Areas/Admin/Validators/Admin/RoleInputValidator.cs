using FluentValidation;
using HbCrm.Web.Areas.Admin.Models.Admin;
using HbCrm.Web.Areas.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Validators.Admin
{
    public class RoleInputValidator:HbCrmAdminBaseValidator<RoleInput>
    {
        public RoleInputValidator()
        {
            RuleFor(c => c.RoleName).NotEmpty().WithMessage("角色名称不能为空");
            RuleFor(c => c.RoleStatus).NotEmpty().WithMessage("角色状态不能为空");
            //RuleFor(c => c.RoleRemark).NotEmpty().WithMessage("密码不能为空");
            //RuleFor(c => c.AdminIds).NotEmpty().WithMessage("必须分配一个角色");
        }
    }
}
