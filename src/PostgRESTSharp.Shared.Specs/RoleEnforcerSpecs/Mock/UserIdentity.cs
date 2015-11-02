using System.Collections.Generic;
using Nancy.Security;

namespace PostgRESTSharp.Shared.Specs.RoleEnforcerSpecs.Mock
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
