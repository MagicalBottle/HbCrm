using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Authorize
{

    public class SysMenuMap : EntityTypeConfiguration<SysMenu>
    {
        public override void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable("sys_menu");
            builder.HasKey(model => model.Id);
            builder.Ignore(model => model.ChildrenMenus);
            builder.Ignore(model => model.Deep);
            builder.Ignore(model => model.ParentMenu);
            builder.Ignore(model => model.Active);
        }
    }
}
