using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            client.Authenticator = authenticator;
            client.BaseUrl = new Uri(baseUrl);
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

        public T ExecuteGet<T>(string resource, string baseUrl, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null) where T : new()
        {
            restRequest.Resource = PrefixResourceIfNecessary(baseUrl, resource);

            AddHeaders(requestHeaders);

            AddQuery(queryStringParameters);

            return Execute<T>(restRequest, baseUrl, authenticator);
        }

        public IRestResponse ExecutePost(string resource, string baseUrl, string requestBody, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null, IAuthenticator authenticator = null)
        {
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = authenticator;

            AddHeaders(requestHeaders);

            AddQuery(queryStringParameters);

            restRequest.Resource = PrefixResourceIfNecessary(baseUrl, resource);

            restRequest.Method = Method.POST;
            restRequest.AddJsonBody(requestBody);

            return client.Execute(restRequest);
        }

        private static string PrefixResourceIfNecessary(string baseUrl, string resource)
        {
            return baseUrl.EndsWith("/") ? "/" + resource : resource;
        }

        private void AddQuery(IEnumerable<KeyValuePair<string, string>> queryStringParameters)
        {
            foreach (var item in queryStringParameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                this.restRequest.AddQueryParameter(item.Key, item.Value);
            }
        }

        private void AddHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders)
        {
            foreach (var item in requestHeaders ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            {
                this.restRequest.AddHeader(item.Key, string.Join("; ", item.Value));
            }
        }
    }
}
