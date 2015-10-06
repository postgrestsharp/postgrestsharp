using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nancy;
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

        public IRestResponse Execute(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null)
        {
            return client.Execute(restRequest);
        }

        public T Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null)
        {
            var response = client.Execute(restRequest);
            var models = JsonConvert.DeserializeObject<T>(response.Content);
            if (response.ErrorException != null)
            {
                //TODO: Custom Error Handling
                //statusCodeHandler.Handle(xxx);
                throw response.ErrorException;
            }
            return models;
        }

        public T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IAuthenticator authenticator = null) where T : new()
        {
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = authenticator;

            restRequest.Resource = baseUrl.EndsWith("/") ? "/" + resource : resource;

            foreach (var item in queryStringParameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                restRequest.AddQueryParameter(item.Key, item.Value);
            }

            return Execute<T>(restRequest, baseUrl, authenticator);
        }

        public IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IAuthenticator authenticator = null)
        {
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = authenticator;

            restRequest.Resource = baseUrl.EndsWith("/") ? "/" + resource : resource;

            restRequest.Method = Method.POST;
            restRequest.AddJsonBody(requestBody);

            return client.Execute(restRequest);
        }
    }
}
