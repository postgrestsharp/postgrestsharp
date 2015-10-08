using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public class AuthenticatorFactory : IAuthenticatorFactory
    {
        public IAuthenticator GetPostgrestAuthenticator(string dbUser, string password)
        {
            return new HttpBasicAuthenticator(dbUser, password);
        }
    }
}