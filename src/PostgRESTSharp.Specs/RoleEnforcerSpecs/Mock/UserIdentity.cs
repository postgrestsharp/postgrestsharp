using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Principal;
using Nancy.Extensions;
using Nancy.Security;
using NSubstitute;

namespace PostgRESTSharp.Specs
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
