using System.Collections.Generic;
using System.Dynamic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy.Json;
using Newtonsoft.Json;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.JavaScriptConverter.Serialising
{
    public class when_serialising_a_rest_link_uri : WithFakes
    {
        Establish that = () =>
        {
            href = "http://localhosty:1234/api/banana";

            link = new SimpleRestLink(href);

            converter = new RestLinksJavaScriptConverter();
        };

        Because of = () =>
        {
            values = converter.Serialize(link, new JavaScriptSerializer());
            json = JsonConvert.SerializeObject(link, Formatting.Indented);
            jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
        };

        It should_have_returned_a_result = () =>
        {
            values.ShouldNotBeEmpty();
        };

        It should_only_have_the_href_property_in_the_first_link = () =>
        {
            values.Count.ShouldEqual(1);
        };

        It should_have_an_href_with_expected_url = () =>
        {
            values["href"].ShouldEqual(href);
        };

        It should_have_similar_values_to_the_newtonsoft_JsonConverter_result = () =>
        {
            (jsonObject.href as string).ShouldEqual(values["href"]);
        };

        static IRestSimpleLink link;
        static string href;
        static string json;
        static dynamic jsonObject;
        private static RestLinksJavaScriptConverter converter;
        private static IDictionary<string, object> values;
    }
}
