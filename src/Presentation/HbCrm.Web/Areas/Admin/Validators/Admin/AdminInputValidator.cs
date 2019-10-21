using HbCrm.Web.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HbCrm.Web.Areas.Validators.Admin
{
    public class AdminInputValidator: HbCrmAdminBaseValidator<AdminInput>
    {
        public AdminInputValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("登录名不能为空");
            RuleFor(c => c.NickName).NotEmpty().WithMessage("登录名不能为空");
            RuleFor(c => c.Password).NotEmpty().WithMessage("密码不能为空");
            RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("再次确认密码不能为空").Equal(c=>c.Password).WithMessage("两次输入的密码不一致");
            //RuleFor(c => c.MobilePhone).NotEmpty().WithMessage("手机号不能为空");
            //RuleFor(c => c.Email).NotEmpty().WithMessage("登录名不能为空");
            //RuleFor(c => c.QQ).NotEmpty().WithMessage("登录名不能为空");
            //RuleFor(c => c.WeChar).NotEmpty().WithMessage("登录名不能为空");
            RuleFor(c => c.RoleIds).NotEmpty().WithMessage("必须分配一个角色");
        }
    }
}
