using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Specs.ApiClientSpecs
{
    public class when_calling_execute_get_with_headers : WithFakes
    {

        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse restResponse;
        private static string url = "http://test.com/";
        private static string endpointResource = "test";
        private static SimpleTest test;

        Establish that = () =>
        {
            restRequest = An<IRestRequest>();
            restRequest.WhenToldTo(a => a.Parameters).Return(new List<Parameter>());
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse>();
            restResponse.WhenToldTo(x => x.Content).Return(@"{ id: 1, Description: 'Description' }");

            restClient.WhenToldTo(x => x.Execute(restRequest)).Return(restResponse);

            restRequestFactory = An<IRestRequestFactory>();
            restRequestFactory.WhenToldTo(a => a.Create()).Return(restRequest);

            serialiser = An<ISerializer>();
            deserialiser = An<IDeserializer>();

            apiClient = new ApiClient(restClient, restRequestFactory, serialiser, deserialiser);

            headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers.Add(new KeyValuePair<string, IEnumerable<string>>("SomeHeader1", new[] { "1", "2" }));
            headers.Add(new KeyValuePair<string, IEnumerable<string>>("SomeHeader2", new[] { "banana" }));
        };

        public Because of = async () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            test = await apiClient.ExecuteGetAsync<SimpleTest>(endpointResource, url, null, headers, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_resource_set_on_request = () => restRequest.Resource.ShouldEqual(endpointResource);

        public It should_have_method_set_to_get = () => restRequest.Method.ShouldEqual(Method.GET);

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync<SimpleTest>(restRequest));

        public It should_have_supplied_first_header_values_to_the_underlying_request = () =>
        {
            restRequest.WasToldTo(a => a.AddHeader(headers[0].Key, string.Join("; ", headers[0].Value)));
        };

        public It should_have_supplied_the_second_header_values_to_the_underlying_request = () =>
        {
            restRequest.WasToldTo(a => a.AddHeader(headers[1].Key, string.Join("; ", headers[1].Value)));
        };

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;
        private static List<KeyValuePair<string, IEnumerable<string>>> headers;
    }
}
