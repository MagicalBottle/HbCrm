using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Core.Domain.DataEnumerate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Authorize
{

    public class SysMenuMap : EntityTypeConfiguration<SysMenu>
    {
        public override void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable("sys_menu");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MenuName).HasMaxLength(50).IsRequired();
            builder.Property(m => m.MenuSystermName).HasMaxLength(255).IsRequired();
            builder.Property(m => m.ParentMenuId).HasDefaultValue(0).IsRequired();
            builder.Property(m => m.Type).HasDefaultValue((int)MenuType.Menu).IsRequired();

            builder.Property(m => m.MenuIcon).HasMaxLength(50);
            builder.Property(m => m.MenuSort).HasDefaultValue(0).IsRequired();
            builder.Property(m => m.MenuRemark).HasMaxLength(255);
            builder.Property(m => m.MenuUrl).HasMaxLength(255);


            builder.Ignore(m => m.MenuType);
            builder.Ignore(m => m.ChildrenMenus);
            builder.Ignore(m => m.ParentMenu);
            builder.Ignore(m => m.Deep);
            builder.Ignore(m => m.Active);
        }
    }
}
