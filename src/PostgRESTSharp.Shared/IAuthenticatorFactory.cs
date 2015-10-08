using System.Security.Cryptography.X509Certificates;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IAuthenticatorFactory
    {
        IAuthenticator GetPostgrestAuthenticator(string dbUser, string password);
    }
}