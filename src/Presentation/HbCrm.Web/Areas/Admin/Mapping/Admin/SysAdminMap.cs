using AutoMapper;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Core.Domain.DataEnumerate;
using HbCrm.Web.Areas.Admin.Models;
using HbCrm.Web.Areas.Admin.Models.Admin;
using HbCrm.Web.Areas.Admin.Models.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Mapping.Admin
{
    /// <summary>
    /// 所有的对象映射放这里
    /// </summary>
    public class SysAdminMap : Profile
    {
        public SysAdminMap()
        {
            CreateMap<AdminInput, SysAdmin>()
                .ForMember(to => to.Guid, option => option.Ignore());

            CreateMap<SysAdmin, SelectOutPut>()
               .ForMember(to => to.id, opt => opt.MapFrom(from => from.Id))
               .ForMember(to => to.text, opt => opt.MapFrom(from => from.UserName));

            CreateMap<SysRole, SelectOutPut>()
               .ForMember(to => to.id, opt => opt.MapFrom(from => from.Id))
               .ForMember(to => to.text, opt => opt.MapFrom(from => from.RoleName));


            CreateMap<RoleInput, SysRole>()
                .ForMember(to => to.AdminRoles, option => option.Ignore())
                .ForMember(to => to.RoleStatus, opt => opt.MapFrom((source, destination)=> { return source.RoleStatus == 1 ? RoleStatus.Active : RoleStatus.Locked; }));


            CreateMap<MenuInput, SysMenu>()
                .ForMember(to => to.Deep, option => option.Ignore());

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
