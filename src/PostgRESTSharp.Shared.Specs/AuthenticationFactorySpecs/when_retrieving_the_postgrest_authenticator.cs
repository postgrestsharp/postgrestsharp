using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Specs.AuthenticationFactorySpecs
{
    public class when_retrieving_the_postgrest_authenticator : WithFakes
    {
        Establish that = () =>
        {
            factory = new AuthenticatorFactory();
        };

        private Because of = () =>
        {
            authenticator = factory.GetPostgrestAuthenticator("someone", "somepassword");
        };

        private It should_have_returned_an_authenticator = () =>
        {
            authenticator.ShouldNotBeNull();
        };

        private static AuthenticatorFactory factory;
        private static IAuthenticator authenticator;
    }
}
