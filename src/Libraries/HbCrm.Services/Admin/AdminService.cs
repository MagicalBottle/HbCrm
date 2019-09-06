using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using System.Linq;

namespace HbCrm.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<HbCrm.Core.Domain.Admin.Admin> _adminRepository;
        public AdminService(IRepository<HbCrm.Core.Domain.Admin.Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Core.Domain.Admin.Admin GetAdmin(string userName)
        {
            return _adminRepository.Table.FirstOrDefault(a => a.UserName == userName);
        }
    }
}
