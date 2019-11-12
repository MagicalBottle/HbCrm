using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HbCrm.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult<string> Login(string name)
        {
            return "ok";
        }

        
        public ActionResult<string> Logout(string name)
        {
            return "logout";
        }
    }
}