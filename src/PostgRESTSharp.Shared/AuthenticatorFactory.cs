using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public class AuthenticatorFactory : IAuthenticatorFactory
    {
        public IPostgrestAuthenticator GetPostgrestAuthenticator(string dbUser, string password)
        {
            return (IPostgrestAuthenticator) new HttpBasicAuthenticator(dbUser, password);
        }
    }
}