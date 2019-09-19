using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class AdminLoginSuccessModel
    {
        public LoginStatus LoginStatus { get; set; }
        
    }

    public enum LoginStatus
    {
        Error = 0,
        Success = 1,
        Locked = 2
    }
}
