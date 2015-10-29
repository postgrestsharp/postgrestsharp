using System.Dynamic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_serialising_a_templated_link_uri : WithFakes
    {
        Establish that = () =>
        {
            href = "http://localhosty:1234/api/banana";
            name = "monkey";
            templated = true;

            link = new SimpleRestLink(name, href, templated);
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

        It should_have_three_properties = () =>
        {
            var propertyCount = (int)Enumerable.Count(jsonObject);
            propertyCount.ShouldEqual(3);
        };

        It should_have_an_href_with_expected_url = () =>
        {
            var value = jsonObject.href as string;
            value.ShouldEqual(href);
        };

        It should_have_a_name_with_the_expected_value = () =>
        {
            var value = jsonObject.name as string;
            value.ShouldEqual(name);
        };

        It should_have_a_templated_property_with_the_expected_value = () =>
        {
            var value = (bool)jsonObject.templated;
            value.ShouldEqual(templated);
        };

        static SimpleRestLink link;
        static string href;
        static string json;
        static dynamic jsonObject;
        private static string name;
        private static bool templated;
    }
}
