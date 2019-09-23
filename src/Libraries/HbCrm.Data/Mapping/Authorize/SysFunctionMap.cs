using HbCrm.Core.Domain.Authorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Data.Mapping.Authorize
{
    public class SysFunctionMap : EntityTypeConfiguration<SysFunction>
    {
        public override void Configure(EntityTypeBuilder<SysFunction> builder)
        {
            builder.ToTable("sys_function");
            builder.HasKey(model => model.Id);
        }
    }
}
