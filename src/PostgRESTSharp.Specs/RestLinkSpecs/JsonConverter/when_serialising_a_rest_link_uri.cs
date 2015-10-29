using System.Dynamic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_serialising_a_rest_link_uri : WithFakes
    {
        Establish that = () =>
        {
            href = "http://localhosty:1234/api/banana";

            link = new SimpleRestLink(href);
        };

        Because of = () =>
        {
            json = JsonConvert.SerializeObject(link, Formatting.Indented);
            jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
        };

        It should_have_returned_a_result = () =>
        {
            json.ShouldNotBeEmpty();
        };

        It should_only_have_the_href_property_in_the_first_link = () =>
        {
            var propertyCount = (int)Enumerable.Count(jsonObject);
            propertyCount.ShouldEqual(1);
        };

        It should_have_an_href_with_expected_url = () =>
        {
            var value = jsonObject.href as string;
            value.ShouldEqual(href);
        };

        static IRestSimpleLink link;
        static string href;
        static string json;
        static dynamic jsonObject;
    }
}
