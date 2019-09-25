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

            builder.Ignore(model => model.Roles);
            builder.Ignore(model => model.Menus);
            builder.Ignore(model => model.Functions);
        }
    }
}
