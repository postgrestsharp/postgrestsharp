using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        T ExecuteGet<T>(string request, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IAuthenticator authenticator = null) where T : new();
        IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IAuthenticator authenticator = null);
    }
}