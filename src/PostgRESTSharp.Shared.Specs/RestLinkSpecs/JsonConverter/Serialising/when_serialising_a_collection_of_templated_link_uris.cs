using System.Dynamic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Serialising
{
    public class when_serialising_a_collection_of_templated_link_uris : WithFakes
    {
        Establish that = () =>
        {
            selfHref = "http://localhosty:1234/api/banana";
            selfName = "name1";
            selfTemplated = true;
            link = new SimpleRestLink(selfName, selfHref, selfTemplated);

            relatedHref = "http://localhosty:1234/api/fruit";
            relatedName = "name2";
            relatedTemplated = false;
            relatedLink = new SimpleRestLink(relatedName, relatedHref, relatedTemplated);

            links = new RestLinks();
            links.Add("self", link);
            links.Add("fruit", relatedLink);
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

        It should_have_three_properties_in_the_self_link = () =>
        {
            var value = (int)Enumerable.Count(jsonObject.self);
            value.ShouldEqual(3);
        };

        It should_have_a_self_link_with_the_expected_url = () =>
        {
            var value = jsonObject.self.href as string;
            value.ShouldEqual(selfHref);
        };

        It should_have_a_self_link_with_the_expected_name = () =>
        {
            var value = jsonObject.self.name as string;
            value.ShouldEqual(selfName);
        };

        It should_have_a_self_link_with_the_exptected_templated_value = () =>
        {
            var value = (bool)jsonObject.self.templated;
            value.ShouldEqual(selfTemplated);
        };

        It should_have_the_self_link_as_the_first_link_in_the_collection = () =>
        {
            var firstValue = Enumerable.First(jsonObject).Value as string;
            var selfValue = jsonObject.self as string;

            firstValue.ShouldBeTheSameAs(selfValue);
        };

        It should_have_three_properties_in_the_related_link = () =>
        {
            var value = (int)Enumerable.Count(jsonObject.fruit);
            value.ShouldEqual(3);
        };

        It should_have_a_related_link_with_the_expected_url = () =>
        {
            var value = jsonObject.fruit.href as string;
            value.ShouldEqual(relatedHref);
        };

        It should_have_a_related_link_with_the_expected_name = () =>
        {
            var value = jsonObject.fruit.name as string;
            value.ShouldEqual(relatedName);
        };

        It should_have_a_related_link_with_the_exptected_templated_value = () =>
        {
            var value = (bool)jsonObject.fruit.templated;
            value.ShouldEqual(relatedTemplated);
        };

        static IRestSimpleLink link;
        static string selfHref;
        static string json;
        static RestLinks links;
        static SimpleRestLink relatedLink;
        static dynamic jsonObject;
        static string relatedHref;
        private static string selfName;
        private static string relatedName;
        private static bool selfTemplated;
        private static bool relatedTemplated;
    }
}
