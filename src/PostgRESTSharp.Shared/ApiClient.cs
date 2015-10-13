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
        protected IRestClient client;
        protected IRestRequest restRequest;

        public ApiClient(IRestClient client, IRestRequest restRequest)
        {
            this.client = client;
            this.restRequest = restRequest;
        }

        public IRestResponse Execute(string resource, string baseUrl, IAuthenticator authenticator = null, IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders = null)
        {
            client.Authenticator = authenticator;
            client.BaseUrl = new Uri(baseUrl);
            restRequest.Resource = PrefixResourceIfNecessary(baseUrl, resource);
            AddHeaders(requestHeaders);
            AddQuery(queryStringParameters);
            return client.Execute(restRequest);
        }

        public T Execute<T>(IRestRequest restRequest, string baseUrl, IAuthenticator authenticator = null)
        {
            client.Authenticator = authenticator;
            client.BaseUrl = new Uri(baseUrl);
            var restResponse = client.Execute(restRequest);
            var models = JsonConvert.DeserializeObject<T>(restResponse.Content);
            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
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

        protected virtual string PrefixResourceIfNecessary(string baseUrl, string resource)
        {
            return baseUrl.EndsWith("/") ? "/" + resource : resource;
        }

        protected virtual void AddQuery(IEnumerable<KeyValuePair<string, string>> queryStringParameters)
        {
            foreach (var item in queryStringParameters ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                restRequest.AddQueryParameter(item.Key, item.Value);
            }
        }

        protected virtual void AddHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> requestHeaders)
        {
            foreach (var item in requestHeaders ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            {
                restRequest.AddHeader(item.Key, string.Join("; ", item.Value));
            }
        }
    }
}
