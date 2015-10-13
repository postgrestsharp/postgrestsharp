using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nancy.ErrorHandling;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public class ApiClient : IApiClient
    {
        private readonly IRestClient client;
        private readonly IRestRequest restRequest;

        public ApiClient(IRestClient client, IRestRequest restRequest)
        {
            this.client = client;
            this.restRequest = restRequest;
        }

        public async Task<T> Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null)
        {
            client.Authenticator = authenticator;
            client.BaseUrl = new Uri(baseUrl);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(restRequest);
            var models = JsonConvert.DeserializeObject<T>(response.Content);
            if (response.ErrorException != null)
            {
                //TODO: Custom Error Handling
                //statusCodeHandler.Handle(xxx);
                throw response.ErrorException;
            }
            return models;
        }

        public async Task<T> ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IAuthenticator authenticator = null) where T : new()
        {
            restRequest.Resource = baseUrl.EndsWith("/") ? "/" + resource : resource;

            foreach (var item in queryStringParameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                restRequest.AddQueryParameter(item.Key, item.Value);
            }

            return await Execute<T>(restRequest, baseUrl, authenticator);;
        }

        //TODO: return T instead of IRestResponse
        public async Task<IRestResponse> ExecutePost(string resource, string baseUrl, string requestBody, IAuthenticator authenticator = null)
        {
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = authenticator;
            var cancellationTokenSource = new CancellationTokenSource();

            restRequest.Resource = baseUrl.EndsWith("/") ? "/" + resource : resource;

            restRequest.Method = Method.POST;
            restRequest.AddJsonBody(requestBody);

            return await client.ExecuteTaskAsync(restRequest, cancellationTokenSource.Token); ;
        }
    }
}