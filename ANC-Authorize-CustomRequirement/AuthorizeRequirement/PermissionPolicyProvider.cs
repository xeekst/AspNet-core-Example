using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ANC_Authorize_CustomRequirement.AuthorizeRequirement
{
    internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "Permission";
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if(policyName.StartsWith(POLICY_PREFIX,System.StringComparison.OrdinalIgnoreCase)){
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName.Substring(POLICY_PREFIX.Length)));
                return Task.FromResult(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}