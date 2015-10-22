using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Security;

namespace PostgRESTSharp.Shared
{
    public class RoleEnforcer : IRoleEnforcer
    {
        public void EnsureUserBelongsToRoles(INancyModule module, IEnumerable<string> roles)
        {
            module.RequiresClaims(roles.Select(a => "db_role:" + a));
        }

        public void EnsureUserBelongsToAtLeastOneRole(INancyModule module, IEnumerable<string> roles)
        {
            module.RequiresAnyClaim(roles.Select(a => "db_role:" + a));
        }
    }
}