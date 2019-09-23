using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
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
            //builder.Ignore(model => model.AdminRoles);

            //builder.Ignore(modle => modle.Roles);
            //builder.Property(admin => admin.CreatebyName).HasMaxLength(50);
            //builder.Property(admin => admin.Email).HasMaxLength(200);
            //builder.Property(admin => admin.Guid).HasMaxLength(64);
            //builder.Property(admin => admin.LastUpdateByName).HasMaxLength(50);
            //builder.Property(admin => admin.MobilePhone).HasMaxLength(20);
            //builder.Property(admin => admin.NickName).HasMaxLength(50);
            //builder.Property(admin => admin.Password).HasMaxLength(64);
            //builder.Property(admin => admin.UserName).HasMaxLength(50);
        }
    }
}
