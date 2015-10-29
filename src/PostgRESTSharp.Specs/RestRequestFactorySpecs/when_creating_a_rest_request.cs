using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;

namespace PostgRESTSharp.Specs.RestRequestFactorySpecs
{
    public class when_creating_a_rest_request : WithFakes
    {
        Establish that = () =>
        {
            constructionAction = An<Func<IRestRequest>>();
            request = An<IRestRequest>();
            constructionAction.WhenToldTo(a => a.Invoke()).Return(request);
            factory = new RestRequestFactory(constructionAction);
        };

        private Because of = () =>
        {
            result = factory.Create();
        };

        private It should_have_called_the_construction_action = () =>
        {
            constructionAction.WasToldTo(a => a.Invoke()).OnlyOnce();
        };

        private It should_have_constructed_a_new_request = () =>
        {
            result.ShouldNotBeNull();
        };

        private It should_be_the_expected_request = () =>
        {
            result.ShouldBeTheSameAs(request);
        };

        private static RestRequestFactory factory;
        private static IRestRequest result;
        private static Func<IRestRequest> constructionAction;
        private static IRestRequest request;
    }
}
