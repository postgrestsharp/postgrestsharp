using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        Task<T> ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null) where T : new();
        Task<IRestResponse> ExecutePost(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        Task<T> Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
        Task<IRestResponse> Execute(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null);
    }
}