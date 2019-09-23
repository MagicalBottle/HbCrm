using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;

namespace HbCrm.Services.Admin
{
   public interface IAdminService
    {
        HbCrm.Core.Domain.Admin.SysAdmin GetAdminByUserName(string userName);
    }
}
