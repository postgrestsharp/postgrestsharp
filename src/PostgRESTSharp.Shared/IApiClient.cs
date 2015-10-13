using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        Task<T> ExecuteGetAsync<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        Task<IRestResponse> ExecutePostAsync(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        Task<T> ExecuteAsync<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
        Task<IRestResponse> ExecuteAsync(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null);

        T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        T Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
        IRestResponse Execute(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null);

    }
}