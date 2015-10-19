using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public class ApiClient : IApiClient
    {
        protected IRestClient client;
        protected IRestRequestFactory restRequestFactory;

        public ApiClient(IRestClient client, IRestRequestFactory restRequestFactory)
        {
            this.client = client;
            this.restRequestFactory = restRequestFactory;
        }

        public virtual T Execute<T>(IRestRequest request, string baseUrl, IAuthenticator authenticator = null)
        {
            var responseTask = ExecuteAsync(request, baseUrl, authenticator);
            responseTask.Wait();
            return GetContentAs<T>(responseTask.Result);
        }

        public virtual async Task<T> ExecuteAsync<T>(IRestRequest request, string baseUrl, IAuthenticator authenticator = null)
        {
            var response = await ExecuteAsync(request, baseUrl, authenticator);
            return GetContentAs<T>(response);
        }

        public virtual IRestResponse Execute(IRestRequest request, string baseUrl, IAuthenticator authenticator = null)
        {
            var task = ExecuteAsync(request, baseUrl, authenticator);
            task.Wait();
            return task.Result;
        }

        public virtual Task<IRestResponse> ExecuteAsync(IRestRequest request, string baseUrl, IAuthenticator authenticator = null)
        {
            client.Authenticator = authenticator;
            client.BaseUrl = new Uri(baseUrl);
            return client.ExecuteTaskAsync(request);
        }

        public virtual async Task<IApiResponse<T>> ExecuteWrappedAsync<T>(IRestRequest request, string baseUrl, IAuthenticator authenticator = null)
        {
            var response = await ExecuteAsync(request, baseUrl, authenticator);
            var data = default(T);
            if (!string.IsNullOrEmpty(response.Content)) //we might not be interested in the Content of our request
            {
                try
                {
                    data = JsonConvert.DeserializeObject<T>(response.Content);
                }
                catch (Exception)
                {
                    //swallow
                }
            }
            return new ApiResponse<T>(response, data);
        }

        public virtual async Task<IApiResponse<T>> ExecuteWrappedAsync<T>(string resource, string baseUrl, IAuthenticator authenticator = null,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, Method method = Method.GET, object requestBody = null,
            DataFormat requestFormat = DataFormat.Json)
        {
            var request = CreateRequest(resource, baseUrl, queryStringParameters, requestHeaders, method, requestBody, requestFormat);
            return await ExecuteWrappedAsync<T>(request, baseUrl, authenticator);
        }

        public virtual async Task<IRestResponse> ExecuteAsync(string resource, string baseUrl, IAuthenticator authenticator = null,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, Method method = Method.GET)
        {
            var request = CreateRequest(resource, baseUrl, queryStringParameters, requestHeaders, method);
            return await ExecuteAsync(request, baseUrl, authenticator);
        }

        public virtual T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null)
        {
            var task = ExecuteGetAsync<T>(resource, baseUrl, queryStringParameters, requestHeaders, authenticator);
            task.Wait();
            return task.Result;
        }

        public virtual async Task<T> ExecuteGetAsync<T>(string resource, string baseUrl,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null)
        {
            var result = await ExecuteWrappedAsync<T>(resource, baseUrl, authenticator, queryStringParameters, requestHeaders);
            return result.Data;
        }

        public virtual IRestResponse ExecutePost(string resource, string baseUrl, string requestBody,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null)
        {
            var task = ExecutePostAsync(resource, baseUrl, requestBody, queryStringParameters, requestHeaders, authenticator);
            task.Wait();
            return task.Result;
        }

        public virtual async Task<IRestResponse> ExecutePostAsync(string resource, string baseUrl, string requestBody,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null)
        {
            var request = CreateRequest(resource, baseUrl, queryStringParameters, requestHeaders, Method.POST, requestBody);
            return await ExecuteAsync(request, baseUrl, authenticator);
        }

        protected virtual IRestRequest CreateRequest(string resource, string baseUrl,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, Method method = Method.GET, object requestBody = null,
            DataFormat requestFormat = DataFormat.Json)
        {
            var request = restRequestFactory.Create();
            request.Resource = PrefixResourceIfNecessary(baseUrl, resource);
            request.Method = method;
            request.RequestFormat = requestFormat;
            if (requestBody != null)
            {
                request.AddBody(requestBody);
            }
            foreach (var item in requestHeaders ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            {
                request.AddHeader(item.Key, string.Join("; ", item.Value));
            }
            foreach (var item1 in queryStringParameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                request.AddQueryParameter(item1.Key, item1.Value);
            }
            return request;
        }

        protected virtual T GetContentAs<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        protected virtual string PrefixResourceIfNecessary(string baseUrl, string resource)
        {
            return baseUrl.EndsWith("/") ? resource : "/" + resource;
        }
    }
}
