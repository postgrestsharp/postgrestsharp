using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.Builder
{
    public class when_building_a_self_link_for_a_new_entity : WithFakes
    {
        Establish that = () =>
        {
            builder = new RestLinkBuilder();
            links = new RestLinks();
            url = new Url("http://somewhere.com/");
            entityId = "10";
        };

        private Because of = () =>
        {
            builder.AddSelfLinkForNewEntity(links, url, entityId);
        };

        private It should_have_added_a_self_link = () =>
        {
            links.First().Key.ShouldEqual("self");
        };

        private It should_have_the_expected_value = () =>
        {
            links.First().Value.ShouldMatch(a => RestLinkMatcher(a));
        };

        private static bool RestLinkMatcher(IRestLink restLink)
        {
            var link = restLink as SimpleRestLink;
            return link != null && link.Uri.Href.Equals(url + "/" + entityId);
        }

        private static RestLinkBuilder builder;
        private static RestLinks links;
        private static Url url;
        private static string entityId;
    }
}