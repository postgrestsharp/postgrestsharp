using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Specs.ConventionResolverSpecs
{
    public class when_calling_execute_get_with_query_no_parameters : WithFakes
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
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse>();
            restResponse.WhenToldTo(x => x.Content).Return(@"{ id: 1, Description: 'Description' }");

            restClient.WhenToldTo(x => x.Execute(restRequest)).Return(restResponse);

            apiClient = new ApiClient(restClient, restRequest);
        };

        public Because of = () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            test = await apiClient.ExecuteGet<SimpleTest>(endpointResource, url, null, null, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_resource_set_on_request = () => restRequest.Resource.ShouldEqual("/" + endpointResource);

        public It should_have_method_set_to_get = () => restRequest.Method.ShouldEqual(Method.GET);

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.Execute(restRequest));

    }
}
