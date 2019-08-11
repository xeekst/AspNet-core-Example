using Microsoft.AspNetCore.Authorization;

namespace ANC_Authorize_CustomRequirement.AuthorizeRequirement
{
    internal class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Permission";

        public PermissionAuthorizeAttribute(string permissionName) => PermissionName = permissionName;
        public string PermissionName
        {
            get
            {
                return PermissionName;
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}