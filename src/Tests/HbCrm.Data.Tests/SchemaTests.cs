using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HbCrm.Core;
using HbCrm.Data;
using Microsoft.EntityFrameworkCore;

namespace HbCrm.Data.Tests
{
    public class SchemaTests
    {
        [Fact]
        public void Can_Generate_Schema()
        {
            DbContextOptions<HbCrmContext> options = new DbContextOptions<HbCrmContext>();

            HbCrmContext context = new HbCrmContext(options);
           // string script=context.GenerateCreateScript();

        }
    }
}
