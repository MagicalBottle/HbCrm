using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.DataEnumerate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Admin
{
    public class SysAdminMap : EntityTypeConfiguration<SysAdmin>
    {
        public override void Configure(EntityTypeBuilder<SysAdmin> builder)
        {
            builder.ToTable("sys_admin");
            builder.HasKey(model => model.Id);
            builder.Property(m => m.Guid).HasMaxLength(50).IsRequired();
            builder.Property(m=>m.UserName).HasMaxLength(50).IsRequired();
            builder.Property(m => m.NickName).HasMaxLength(50);
            builder.Property(m => m.Password).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(100);
            builder.Property(m => m.MobilePhone).HasMaxLength(50);
            builder.Property(m => m.QQ).HasMaxLength(50);
            builder.Property(m => m.WeChar).HasMaxLength(50);
            builder.Property(m => m.Status).HasDefaultValue((int)AdminStatus.Active).IsRequired();


            builder.Ignore(m => m.AdminStatus);
            builder.Ignore(model => model.Roles);
            builder.Ignore(model => model.Menus);
        }
    }
}
