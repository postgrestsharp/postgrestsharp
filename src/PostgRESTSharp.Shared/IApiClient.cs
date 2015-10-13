using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        T Execute<T>(IRestRequest restRequest, string baseUrl, out IRestResponse restResponse, IAuthenticator authenticator = null);
        T ExecuteGet<T>(string resource, string baseUrl, out IRestResponse restResponse, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null) where T : new();
        T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null) where T : new();
        IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);
        T Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
        IRestResponse Execute(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);
    }
}