using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Web
{
   public interface IWorkContext
    {
        HbCrm.Core.Domain.Admin.SysAdmin Admin { get; set; }

        HttpContext HttpContext { get; }
    }
}
