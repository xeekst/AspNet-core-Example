using System.Security.Claims;
using System.Threading.Tasks;
using ANC_Authorize_CustomRequirement.AppCache;
using Microsoft.AspNetCore.Authorization;

namespace ANC_Authorize_CustomRequirement.AuthorizeRequirement
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role);
            if (role != null)
            {
                var roleValue = role.Value;
                var permissions = RolePermissionCache.GetPermissions(role.Value);
                if (permissions.Contains(requirement.PermissionName))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}