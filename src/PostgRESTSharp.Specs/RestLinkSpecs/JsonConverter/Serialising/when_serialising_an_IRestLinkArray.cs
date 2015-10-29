using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;
using PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Mock;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Serialising
{
    public class when_serialising_an_IRestLinkArray : WithFakes
    {
        Establish that = () =>
        {
            link = new RestArrayLink();
            link.Uris = new IRestLinkUri[1];
            link.Uris[0] = new RestLinkUri("http://localhosty/1");
        };

        private Because of = () =>
        {
            json = JsonConvert.SerializeObject(link, Formatting.Indented);
        };

        private It should_not_have_serialised_the_object = () =>
        {
            json.ShouldBeEmpty();
        };

        private static RestArrayLink link;
        private static string json;
        private static dynamic jsonObject;
    }
}