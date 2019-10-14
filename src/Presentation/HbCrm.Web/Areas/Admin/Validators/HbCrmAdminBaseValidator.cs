using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HbCrm.Web.Areas.Validators
{
    public abstract class HbCrmAdminBaseValidator<T>:AbstractValidator<T> where T:class
    {
    }
}
