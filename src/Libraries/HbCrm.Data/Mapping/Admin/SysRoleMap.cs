using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;
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
            builder.Ignore(m => m.Admins);
        }
    }
}
