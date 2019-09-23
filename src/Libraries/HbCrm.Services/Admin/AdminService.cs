using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HbCrm.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<HbCrm.Core.Domain.Admin.SysAdmin> _adminRepository;
        public AdminService(IRepository<HbCrm.Core.Domain.Admin.SysAdmin> adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Core.Domain.Admin.SysAdmin GetAdminByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            
            var query = from c in _adminRepository.Table
                        orderby c.Id
                        where c.UserName == userName
                        select c;
            var sysAdmin = query.FirstOrDefault();
            return sysAdmin;       
        }
    }
}
