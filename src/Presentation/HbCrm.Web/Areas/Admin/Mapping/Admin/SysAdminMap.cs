using AutoMapper;
using HbCrm.Core.Domain.Admin;
using HbCrm.Web.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Mapping.Admin
{
    public class SysAdminMap : Profile
    {
        public SysAdminMap()
        {
            CreateMap<AdminInputModel, SysAdmin>()
                .ForMember(to => to.Guid, option => option.Ignore());


                //.ForMember(to => to.AdminRoles, option => option.MapFrom<List<SysAdminRole>>(
                //    (f, t) =>
                //    {
                //        List<SysAdminRole> result = new List<SysAdminRole>();
                //        foreach (var id in f.RoleIds)
                //        {
                //            SysAdminRole ar = new SysAdminRole();
                //            ar.RoleId = id;
                //            ar.AdminId = id;
                //        }
                //        return result;
                //    }));
        }
    }
}
