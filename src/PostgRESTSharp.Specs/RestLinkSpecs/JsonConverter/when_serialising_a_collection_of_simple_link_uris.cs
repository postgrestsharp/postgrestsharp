using System.Dynamic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_serialising_a_collection_of_simple_link_uris : WithFakes
    {
        Establish that = () =>
        {
            selfHref = "http://localhosty:1234/api/banana";
            link = new SimpleRestLink(selfHref);

            relatedHref = "http://localhosty:1234/api/fruit";
            relationUrl = new SimpleRestLink(relatedHref);

            links = new RestLinks();
            links.Add("self", link);
            links.Add("fruit", relationUrl);
        };

        Because of = () =>
        {
            json = JsonConvert.SerializeObject(links, Formatting.Indented);
            jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
        };

        It should_have_returned_a_result = () =>
        {
            json.ShouldNotBeEmpty();
        };

        It should_have_the_expected_number_of_links = () =>
        {
            var value = (int) Enumerable.Count(jsonObject);
            value.ShouldEqual(links.Count);
        };

        It should_only_have_the_href_property_in_the_first_link = () =>
        {
            var value = (int)Enumerable.Count(jsonObject.self);
            value.ShouldEqual(1);
        };

        It should_have_a_self_link_with_the_expected_url = () =>
        {
            var value = jsonObject.self.href as string;
            value.ShouldEqual(selfHref);
        };

        It should_have_the_self_link_as_the_first_link_in_the_collection = () =>
        {
            var firstValue = Enumerable.First(jsonObject).Value as string;
            var selfValue = jsonObject.self as string;

            firstValue.ShouldBeTheSameAs(selfValue);
        };

        It should_only_have_the_href_property_in_the_second_link = () =>
        {
            var value = (int)Enumerable.Count(jsonObject.fruit);
            value.ShouldEqual(1);
        };

        It should_have_a_related_link_with_the_expected_url = () =>
        {
            var value = jsonObject.fruit.href as string;
            value.ShouldEqual(relatedHref);
        };

        static IRestSimpleLink link;
        static string selfHref;
        static string json;
        static RestLinks links;
        static SimpleRestLink relationUrl;
        static dynamic jsonObject;
        static string relatedHref;
    }
}
