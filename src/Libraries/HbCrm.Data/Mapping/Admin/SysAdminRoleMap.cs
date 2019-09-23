using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Admin
{
    public class SysAdminRoleMap : EntityTypeConfiguration<SysAdminRole>
    {
        public override void Configure(EntityTypeBuilder<SysAdminRole> builder)
        {
            builder.ToTable("sys_adminrole");
            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.SysAdmin).WithMany(model => model.AdminRoles).HasForeignKey(model => model.AdminId);
            builder.HasOne(model => model.SysRole).WithMany(model => model.AdminRoles).HasForeignKey(model => model.RoleId);

        }
    }
}
