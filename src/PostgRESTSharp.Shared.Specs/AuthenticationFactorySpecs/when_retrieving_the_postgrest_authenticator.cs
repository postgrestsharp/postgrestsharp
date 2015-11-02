using Machine.Fakes;
using Machine.Specifications;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared.Specs.AuthenticationFactorySpecs
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
