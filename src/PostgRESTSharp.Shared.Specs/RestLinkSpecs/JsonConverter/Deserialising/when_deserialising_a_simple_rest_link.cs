using System;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.JsonConverter.Deserialising
{
    public class when_deserialising_a_simple_rest_link : WithFakes
    {
        Establish that = () =>
        {
            link = new SimpleRestLink("http://localhosty/1");

            //TODO: should have actual json 'text' and not rely on serialising an object to get its json
            json = JsonConvert.SerializeObject(link);
        };


        private Because of = () =>
        {
            exception = Catch.Exception(() => JsonConvert.DeserializeObject<SimpleRestLink>(json));
        };

        private It should_be_throw_an_exception = () =>
        {
            exception.ShouldNotBeNull();
        };

        private It should_be_a_json_serialisation_exception = () =>
        {
            exception.ShouldBeOfExactType<NotImplementedException>();
        };

        private It should_have_the_expected_exception_message = () =>
        {
            exception.Message.ShouldContain(string.Format("Deserialising {0} is not yet supported", typeof(IRestLink).Name));
        };

        private static SimpleRestLink link;
        private static string json;
        private static Exception exception;
    }
}
