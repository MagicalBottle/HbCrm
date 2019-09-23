using HbCrm.Core.Domain.Authorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Data.Mapping.Authorize
{

    public class SysFunctionRoleMap : EntityTypeConfiguration<SysFunctionRole>
    {
        public override void Configure(EntityTypeBuilder<SysFunctionRole> builder)
        {
            builder.ToTable("sys_functionrole");
            builder.HasKey(model => model.Id);

            builder.HasOne(model => model.SysFunction).WithMany(model => model.FunctionRoles).HasForeignKey(model => model.FunctionId);
            builder.HasOne(model => model.SysRole).WithMany(model => model.FunctionRoles).HasForeignKey(model => model.RoleId);

        }
    }
}
