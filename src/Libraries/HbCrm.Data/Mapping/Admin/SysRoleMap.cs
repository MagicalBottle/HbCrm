using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.DataEnumerate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Admin
{
    public class SysRoleMap : EntityTypeConfiguration<SysRole>
    {
        public override void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.ToTable("sys_role");
            builder.HasKey(model => model.Id);

            builder.Property(m => m.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Status).HasDefaultValue((int)RoleStatus.Active).IsRequired();
            builder.Property(m => m.RoleRemark).HasMaxLength(255);

            builder.Ignore(m => m.RoleStatus);
            builder.Ignore(m => m.Admins);
        }
    }
}
