using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy.Json;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JavaScriptConverter.Deserialising
{
    public class when_serialising_a_templated_link_uri : WithFakes
    {
        Establish that = () =>
        {
            href = "http://localhosty:1234/api/banana";
            name = "monkey";
            templated = true;

            link = new SimpleRestLink(name, href, templated);

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

        It should_have_three_properties = () =>
        {
            values.Count.ShouldEqual(3);
        };

        It should_have_an_href_with_expected_url = () =>
        {
            values["href"].ShouldEqual(href);
        };

        It should_have_a_name_with_the_expected_value = () =>
        {
            values["name"].ShouldEqual(name);
        };

        It should_have_a_templated_property_with_the_expected_value = () =>
        {
            values["templated"].ShouldEqual(templated);
        };

        It should_have_similar_values_to_the_newtonsoft_JsonConverter_result = () =>
        {
            (jsonObject.href as string).ShouldEqual(values["href"]);
            (jsonObject.name as string).ShouldEqual(values["name"]);
            ((bool)jsonObject.templated).ShouldEqual(values["templated"]);
        };

        static SimpleRestLink link;
        static string href;
        static string json;
        static dynamic jsonObject;
        private static string name;
        private static bool templated;
        private static RestLinksJavaScriptConverter converter;
        private static IDictionary<string, object> values;
    }
}
