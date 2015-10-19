using System;
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
        Task<IApiResponse<T>> ExecuteWrappedAsync<T>(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, Method method = Method.GET, object requestBody = null, DataFormat requestFormat = DataFormat.Json);

        //OBSOLETE

        [Obsolete("Use ExecuteWrappedAsync<T> if you wish to access the IRestResponse")]
        Task<IRestResponse> ExecuteAsync(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, Method method = Method.GET);

        [Obsolete("Use async variant ExecuteGetAsync")]
        T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);

        [Obsolete("Use ExecuteWrappedAsync<T> if you wish to access the IRestResponse")]
        IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null);

        [Obsolete("Use alternative methods to allow ApiClient to create your request")]
        T Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null);

        [Obsolete("Use alternative methods to allow ApiClient to create your request")]
        Task<T> ExecuteAsync<T>(IRestRequest request, string baseUrl, IAuthenticator authenticator = null);

        [Obsolete("Use alternative methods to allow ApiClient to create your request")]
        Task<IApiResponse<T>> ExecuteWrappedAsync<T>(IRestRequest request, string baseUrl, IAuthenticator authenticator = null);

        [Obsolete("1) Use alternative methods to allow ApiClient to create your request. 2) Use ExecuteWrappedAsync<T> if you wish to access the IRestResponse")]
        IRestResponse Execute(IRestRequest request, string baseUrl, IAuthenticator authenticator = null);

        [Obsolete("1) Use alternative methods to allow ApiClient to create your request. 2) Use ExecuteWrappedAsync<T> if you wish to access the IRestResponse")]
        Task<IRestResponse> ExecuteAsync(IRestRequest request, string baseUrl, IAuthenticator authenticator = null);
    }
}
