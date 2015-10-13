using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        Task<T> ExecuteGet<T>(string request, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IAuthenticator authenticator = null) where T : new();
        Task<IRestResponse> ExecutePost(string resource, string baseUrl, string requestBody, IAuthenticator authenticator = null);
        Task<T> Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
    }
}