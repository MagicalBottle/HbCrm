using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;

namespace HbCrm.Services.Admin
{
   public interface IAdminService
    {
        SysAdmin GetAdminByUserName(string userName);


       // SysAdmin GetAdminByUserNameNoLazy(string userName);


        SysAdmin GetAdminAllInforByUserName(string userName);
    }
}
