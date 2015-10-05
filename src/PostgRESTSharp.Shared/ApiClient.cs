using System;
using Nancy;
using Nancy.ErrorHandling;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Shared
{
    public interface IApiClient
    {
        T Execute<T>(RestRequest request, string baseUrl, NancyContext context, IAuthenticator authenticator = null) where T : new();
    }

    public class ApiClient : IApiClient
    {
        private readonly IRestClient client;
        private readonly IStatusCodeHandler statusCodeHandler;

        public ApiClient(IRestClient client)
        {
            this.client = client;
            this.statusCodeHandler = statusCodeHandler;

            //this.client.ClearHandlers();
            //this.client.AddHandler("application/json", deserializer);   // Any IDeserializer

        }

        public T Execute<T>(RestRequest request, string baseUrl, NancyContext context, IAuthenticator authenticator = null) where T : new()
        {
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = authenticator;

            var response = client.Execute(request);
            var models = JsonConvert.DeserializeObject<T>(response.Content);

            if (response.ErrorException != null)
            {
                //TODO: Custom Error Handling
                //statusCodeHandler.Handle(xxx);

                throw response.ErrorException;
            }

            return models;
        }
    }
}