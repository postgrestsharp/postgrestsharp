using System;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Specs.ApiClientSpecs
{
    public class when_calling_execute_post : WithFakes
    {

        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse restResponse;
        private static string url = "http://test.com/";
        private static string endpointResource = "test";
        private static string json = @"{ id: 1, Description: 'Description' }";
        
        Establish that = () =>
        {
            restRequest = An<IRestRequest>();
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse>();
            
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
            restResponse = await apiClient.ExecutePostAsync(endpointResource, url, json, null, null, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_resource_set_on_request = () => restRequest.Resource.ShouldEqual(endpointResource);

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_have_method_set_to_post = () => restRequest.Method.ShouldEqual(Method.POST);

        public It should_call_add_json_body = () => restRequest.WasToldTo(x => x.AddBody(json));

        public It should_have_set_the_request_format_to_json = () => restRequest.RequestFormat.ShouldEqual(DataFormat.Json);

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync<object>(restRequest));

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;
    }
}
