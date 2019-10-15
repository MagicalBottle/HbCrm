using AutoMapper;
using HbCrm.Core.Domain.Admin;
using HbCrm.Web.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Mapping.Admin
{
    public class RoleSelectOutPutMap : Profile
    {
        public RoleSelectOutPutMap()
        {
            CreateMap<SysRole, RoleSelectOutPut>()
                .ForMember(to => to.id, opt => opt.MapFrom(from => from.Id))
                .ForMember(to => to.text, opt => opt.MapFrom(from => from.RoleName));
        }
    }
}
