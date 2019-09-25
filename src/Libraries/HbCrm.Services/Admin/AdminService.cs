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
        private readonly IRepository<SysAdmin> _adminRepository;
        public AdminService(IRepository<SysAdmin> adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public SysAdmin GetAdminByUserNameNoLazy(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            _adminRepository.LazyLoadingEnabled(false);//要缓存，必须.Lazy=false 且_adminRepository.Table 且  _adminRepository.Detach(sysAdmin) 
                                                       //另一种方式就是取消懒加载 注释代码option.UseLazyLoadingProxies(); 
            var query = from c in _adminRepository.Table.Include(model => model.AdminRoles)
                        orderby c.Id
                        where c.UserName == userName
                        select c;
            var sysAdmin = query.FirstOrDefault();
            _adminRepository.Detach(sysAdmin);
            return sysAdmin;
        }


        public SysAdmin GetAdminByUserName(string userName)
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
