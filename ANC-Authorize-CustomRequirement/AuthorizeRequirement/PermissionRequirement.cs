using Microsoft.AspNetCore.Authorization;

namespace ANC_Authorize_CustomRequirement.AuthorizeRequirement
{
    public class PermissionRequirement : IAuthorizationRequirement {
        public string PermissionName { get; }

        public PermissionRequirement (string PermissionName) {
            this.PermissionName = PermissionName;
        }
    }
}