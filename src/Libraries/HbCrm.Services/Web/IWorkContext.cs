using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Web
{
   public interface IWorkContext
    {
        HbCrm.Core.Domain.Admin.Admin Admin { get; set; }
    }
}
