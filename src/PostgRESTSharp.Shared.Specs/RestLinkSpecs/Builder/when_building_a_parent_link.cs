using System;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.Builder
{
    public class when_building_a_parent_link : WithFakes
    {
        Establish that = () =>
        {
            builder = new RestLinkBuilder();
            links = new RestLinks();
            url = new Url("http://somewhere.com/a/b/c");
        };

        private Because of = () =>
        {
            builder.AddParentLink(links, url);
        };

        private It should_have_added_a_self_link = () =>
        {
            links.First().Key.ShouldEqual("parent");
        };

        private It should_have_the_expected_value = () =>
        {
            links.First().Value.ShouldMatch(a => RestLinkMatcher(a));
        };

        private static bool RestLinkMatcher(IRestLink restLink)
        {
            var link = restLink as SimpleRestLink;
            var urlWithoutPort = url.ToString().Replace(":80", string.Empty);
            var indexOfLastForwardSlash = urlWithoutPort.LastIndexOf("/");
            var urlWithoutLastSegment = urlWithoutPort.Substring(0, indexOfLastForwardSlash + 1);
            return link != null && link.Uri.Href.Equals(urlWithoutLastSegment);
        }

        private static RestLinkBuilder builder;
        private static RestLinks links;
        private static Url url;
    }
}