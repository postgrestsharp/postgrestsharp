using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Mock;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_serialising_a_type_not_implementing_IRestLink : WithFakes
    {
        Establish that = () =>
        {
            link = new RestLinkThatDoesntImplementIRestLink();
        };

        private Because of = () =>
        {
            json = JsonConvert.SerializeObject(link, Formatting.Indented);
        };

        private It should_not_have_serialised_the_object = () =>
        {
            json.ShouldBeEmpty();
        };

        private static RestLinkThatDoesntImplementIRestLink link;
        private static string json;
        private static dynamic jsonObject;
    }
}
