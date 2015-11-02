using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;

namespace PostgRESTSharp.Specs.PostgRestHttpRequestSpecs
{
    public class when_the_factory_creates_a_request : WithFakes
    {
        Establish that = () =>
        {
            factory = new PostgRestHttpRequestFactory();
        };

        private Because of = () =>
        {
            instance = factory.Create();
        };

        private It should_have_returned_a_new_instance = () =>
        {
            instance.ShouldNotBeNull();
        };

        private It should_be_a_PostgRestHttpRequest = () =>
        {
            instance.ShouldBeOfExactType<PostgRestHttpRequest>();
        };

        private It should_not_be_a_singleton = () =>
        {
            instance.ShouldNotBeTheSameAs(factory.Create());
        };

        private static PostgRestHttpRequestFactory factory;
        private static IHttp instance;
    }
}
