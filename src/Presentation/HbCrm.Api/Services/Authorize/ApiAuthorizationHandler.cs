using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Api.Services.Authorize
{
    public class ApiAuthorizationHandler : AuthorizationHandler<ApiAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext)
                {
                    //var permissionService = mvcContext.HttpContext.RequestServices.GetService<IPermissionService>();

                    //if (permissionService != null && permissionService.Authorize(requirement.Policy))
                    //{
                    //    context.Succeed(requirement);
                    //}
                    //else
                    //{
                    //    context.Fail();
                    //}

                    //默认通过，如果后期加入权限控制
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
