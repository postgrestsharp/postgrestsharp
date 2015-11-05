using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared.Specs.ApiClientSpecs.Mock;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Shared.Specs.ApiClientSpecs
{
    public class when_calling_execute_get_with_query_parameters : WithFakes
    {

        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse restResponse;
        private static string url = "http://test.com/";
        private static string endpointResource = "test";
        private static TestModel testModel;

        Establish that = () =>
        {
            restRequest = An<IRestRequest>();
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse>();
            restResponse.WhenToldTo(x => x.Content).Return(@"{ id: 1, Description: 'Description' }");

            restClient.WhenToldTo(x => x.Execute(restRequest)).Return(restResponse);

            restRequestFactory = An<IRestRequestFactory>();
            restRequestFactory.WhenToldTo(a => a.Create()).Return(restRequest);

            serialiser = An<ISerializer>();
            deserialiser = An<IDeserializer>();

            apiClient = new ApiClient(restClient, restRequestFactory, serialiser, deserialiser);
        };

        public Because of = async () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Id", "1")
            };
            
            testModel = await apiClient.ExecuteGetAsync<TestModel>(endpointResource, url, parameters, null, authenticator);
        };

        public It should_add_query_parameters = () => restRequest.WasToldTo(x => x.AddQueryParameter("Id", "1"));

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_resource_set_on_request = () => restRequest.Resource.ShouldEqual(endpointResource);

        public It should_have_method_set_to_get = () => restRequest.Method.ShouldEqual(Method.GET);

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync<TestModel>(restRequest));

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;
    }
}
