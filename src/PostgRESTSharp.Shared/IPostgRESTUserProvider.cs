using System.Security.Cryptography.X509Certificates;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IPostgRESTUserProvider
    {
        string GetDatabaseUser(INancyModule module);
    }
}