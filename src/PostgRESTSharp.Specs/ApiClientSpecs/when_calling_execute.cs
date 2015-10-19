using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Fakes.Adapters.NSubstitute;
using Machine.Specifications;
using NSubstitute.Core.Arguments;
using NSubstitute.Extensions;
using NUnit.Framework.Constraints;
using NUnit.Specifications;
using NSubstitute;
 
using PostgRESTSharp.Shared;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Specs.ConventionResolverSpecs
{
    public class when_calling_execute : WithFakes
    {

        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse<SimpleTest> restResponse;
        private static string url = "http://test.com/";
        private static SimpleTest test;

        Establish that = () =>
        {
            restRequest = An<IRestRequest>();
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse<SimpleTest>>();
            restResponse.WhenToldTo(x => x.Data).Return(new SimpleTest
            {
                Id = 1,
                Description = "Description"
            });

            var task = StartNewTask();
            restClient.When(x => x.ExecuteTaskAsync<SimpleTest>(restRequest)).Do(y => task.Wait(500));
            restClient.WhenToldTo(x => x.ExecuteTaskAsync<SimpleTest>(restRequest)).Return(task);

            restRequestFactory = An<IRestRequestFactory>();
            restRequestFactory.WhenToldTo(a => a.Create()).Return(restRequest);

            serialiser = An<ISerializer>();
            deserialiser = An<IDeserializer>();

            apiClient = new ApiClient(restClient, restRequestFactory, serialiser, deserialiser);
        };
        
        public Because of = async () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            test = await apiClient.ExecuteAsync<SimpleTest>(restRequest, url, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync<SimpleTest>(restRequest));

        public It should_return_a_model = () =>
        {
            test.Id.ShouldEqual(1);
            test.Description.ShouldEqual("Description");
        };

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;

        public static Task<IRestResponse<SimpleTest>> StartNewTask()
        {
            return Task<IRestResponse<SimpleTest>>.Factory.StartNew(() => restResponse);
        }
        
    }

    public class SimpleTest
    {
        public int Id { get; set; } 
        public string Description { get; set; } 
    }

}
