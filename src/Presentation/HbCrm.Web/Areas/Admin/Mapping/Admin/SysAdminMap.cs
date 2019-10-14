using AutoMapper;
using HbCrm.Core.Domain.Admin;
using HbCrm.Web.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Mapping.Admin
{
    public class SysAdminMap: Profile
    {
        public SysAdminMap()
        {
            CreateMap<AdminInputModel, SysAdmin>()
                .ForMember(m => m.AdminRoles, o => o.Ignore())
                .ForMember(m => m.Guid, o => o.Ignore());
        }
    }
}
