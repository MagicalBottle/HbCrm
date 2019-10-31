using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Api.Configuration
{
    public class HbCrmApiConfiguration
    {
        public Jwt Jwt { get; set; }
    }




    public class Jwt
    {
        /// <summary>
        /// 令牌发行人
        /// </summary>
       public string Issuer { get; set; }

        /// <summary>
        /// 验证架构名称
        /// </summary>
        public string AuthenticateScheme { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string Key { get; set; }
    }
}
