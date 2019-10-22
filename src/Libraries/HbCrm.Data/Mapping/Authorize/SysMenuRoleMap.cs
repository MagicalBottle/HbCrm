using HbCrm.Core.Domain.Authorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Data.Mapping.Authorize
{
    public class SysMenuRoleMap : EntityTypeConfiguration<SysMenuRole>
    {
        public override void Configure(EntityTypeBuilder<SysMenuRole> builder)
        {
            builder.ToTable("sys_menurole");
            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.SysMenu).WithMany(model => model.MenuRoles).HasForeignKey(model => model.MenuId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(model => model.SysRole).WithMany(model => model.MenuRoles).HasForeignKey(model => model.RoleId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
