using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbCrm.Data.Mapping.Admin
{
    public class AdminMap : EntityTypeConfiguration<HbCrm.Core.Domain.Admin.Admin>
    {
        public override void Configure(EntityTypeBuilder<Core.Domain.Admin.Admin> builder)
        {
            builder.HasKey(admin => admin.Id);
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
