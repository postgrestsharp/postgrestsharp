using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using PostgRESTSharp.Shared.Specs.ApiClientSpecs.Mock;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Specs.ApiClientSpecs
{
    public class when_calling_execute_get_with_query_parameters_with_multiple_requests : WithFakes
    {
        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse restResponse;
        private static string url = "http://test.com/";
        private static string endpointResource = "test";
        private static TestModel result1;

        Establish that = async () =>
        {
            restRequest = An<IRestRequest>();
            parameters = new List<Parameter>();
            restRequest.WhenToldTo(a => a.Parameters).Return(parameters);
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse>();
            restResponse.WhenToldTo(x => x.Content).Return(@"{ id: 1, Description: 'Description' }");

            restClient.WhenToldTo(x => x.Execute(restRequest)).Return(restResponse);

            restRequestFactory = An<IRestRequestFactory>();
            restRequestFactory.WhenToldTo(a => a.Create()).Return(restRequest);

            serialiser = An<ISerializer>();
            deserialiser = An<IDeserializer>();

            apiClient = new ApiClient(restClient, restRequestFactory, serialiser, deserialiser);

            authenticator = An<IAuthenticator>();
            var parameters1 = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Id", "1")
            };

            result1 = await apiClient.ExecuteGetAsync<TestModel>(endpointResource, url, parameters1, null, authenticator);

            restRequest.Parameters.ShouldBeEmpty();
        };

        public Because of = async () =>
        {
            var parameters2 = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Description", "2")
            };
            result2 = await apiClient.ExecuteGetAsync<TestModel>(endpointResource, url, parameters2, null, authenticator);
        };

        public It should_add_query_parameters1 = () =>
        {
            restRequest.WasToldTo(x => x.AddQueryParameter("Id", "1"));
        };

        public It should_add_query_parameters2 = () =>
        {
            restRequest.WasToldTo(x => x.AddQueryParameter("Description", "2"));
        };

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;
        private static TestModel result2;
        private static List<Parameter> parameters;
        private static IAuthenticator authenticator;
    }
}