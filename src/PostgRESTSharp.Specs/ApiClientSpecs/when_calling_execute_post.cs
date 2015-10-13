using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;
using RestSharp.Authenticators;

namespace PostgRESTSharp.Specs.ConventionResolverSpecs
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

            apiClient = new ApiClient(restClient, restRequest);
        };

        public Because of = async () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            restResponse = await apiClient.ExecutePost(endpointResource, url, json, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_resource_set_on_request = () => restRequest.Resource.ShouldEqual("/" + endpointResource);

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_have_method_set_to_post = () => restRequest.Method.ShouldEqual(Method.POST);

        public It should_call_add_json_body = () => restRequest.WasToldTo(x => x.AddJsonBody(json));

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync(restRequest, Param.IsAny<CancellationToken>()));

    }
}
