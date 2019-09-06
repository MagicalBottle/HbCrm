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
        }
    }
}
