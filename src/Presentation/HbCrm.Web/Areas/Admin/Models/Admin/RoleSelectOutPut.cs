﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class RoleSelectOutPut
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 对应角色名称
        /// </summary>
        public string text { get; set; }
    }
}