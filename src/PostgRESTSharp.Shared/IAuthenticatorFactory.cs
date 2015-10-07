using System.Security.Cryptography.X509Certificates;

namespace PostgRESTSharp.Shared
{
    public interface IAuthenticatorFactory
    {
        IPostgrestAuthenticator GetPostgrestAuthenticator(string dbUser, string password);
    }
}