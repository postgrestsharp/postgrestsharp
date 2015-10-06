using System.Collections.Generic;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IRoleEnforcer
    {
        void EnsureUserBelongsToRoles(INancyModule module, IEnumerable<string> roles);
    }
}