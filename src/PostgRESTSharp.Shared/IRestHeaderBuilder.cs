using System.Net.Http;

namespace PostgRESTSharp.Shared
{
    public interface IRestHeaderBuilder
    {
        void BuildHeader(HttpClient client, string version, string userId);
    }
}