using System;
using System.Threading.Tasks;
using Machine.Fakes;
using Machine.Specifications;
using NSubstitute;
using PostgRESTSharp.Shared.Specs.ApiClientSpecs.Mock;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PostgRESTSharp.Shared.Specs.ApiClientSpecs
{
    public class when_calling_execute : WithFakes
    {

        private static ApiClient apiClient;
        private static IRestRequest restRequest;
        private static IRestClient restClient;
        private static IRestResponse<TestModel> restResponse;
        private static string url = "http://test.com/";
        private static TestModel testModel;

        Establish that = () =>
        {
            restRequest = An<IRestRequest>();
            restClient = An<IRestClient>();
            restResponse = An<IRestResponse<TestModel>>();
            restResponse.WhenToldTo(x => x.Data).Return(new TestModel
            {
                Id = 1,
                Description = "Description"
            });

            var task = StartNewTask();
            restClient.When(x => x.ExecuteTaskAsync<TestModel>(restRequest)).Do(y => task.Wait(500));
            restClient.WhenToldTo(x => x.ExecuteTaskAsync<TestModel>(restRequest)).Return(task);

            restRequestFactory = An<IRestRequestFactory>();
            restRequestFactory.WhenToldTo(a => a.Create()).Return(restRequest);

            serialiser = An<ISerializer>();
            deserialiser = An<IDeserializer>();

            apiClient = new ApiClient(restClient, restRequestFactory, serialiser, deserialiser);
        };
        
        public Because of = async () =>
        {
            IAuthenticator authenticator = An<IAuthenticator>();
            testModel = await apiClient.ExecuteAsync<TestModel>(restRequest, url, authenticator);
        };

        public It should_have_base_url_set_on_client = () => restClient.BaseUrl.ShouldEqual(new Uri(url));

        public It should_have_authenticator_set = () => restClient.Authenticator.ShouldNotBeNull();

        public It should_execute_call_on_client = () => restClient.WasToldTo(x => x.ExecuteTaskAsync<TestModel>(restRequest));

        public It should_return_a_model = () =>
        {
            testModel.Id.ShouldEqual(1);
            testModel.Description.ShouldEqual("Description");
        };

        private static IRestRequestFactory restRequestFactory;
        private static ISerializer serialiser;
        private static IDeserializer deserialiser;

        public static Task<IRestResponse<TestModel>> StartNewTask()
        {
            return Task<IRestResponse<TestModel>>.Factory.StartNew(() => restResponse);
        }
    }
}
