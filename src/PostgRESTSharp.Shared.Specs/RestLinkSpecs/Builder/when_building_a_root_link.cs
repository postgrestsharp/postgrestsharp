using System;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.Builder
{
    public class when_building_a_root_link : WithFakes
    {
        Establish that = () =>
        {
            builder = new RestLinkBuilder();
            links = new RestLinks();
            url = new Url("http://somewhere.com/");
            rootUrlPart = "someRoot";
            linkName = "someName";
            linkId = "10";
        };

        private Because of = () =>
        {
            builder.AddRootLink(links, url, rootUrlPart, linkName, linkId);
        };

        private It should_have_added_a_self_link = () =>
        {
            links.First().Key.ShouldEqual(linkName);
        };

        private It should_have_the_expected_value = () =>
        {
            links.First().Value.ShouldMatch(a => RestLinkMatcher(a));
        };

        private static bool RestLinkMatcher(IRestLink restLink)
        {
            var link = restLink as SimpleRestLink;
            var uri = new Uri(url);
            var urlWithoutPort = uri.Scheme + "://" + uri.Authority;
            return link != null && link.Uri.Href.Equals(urlWithoutPort + "/" + rootUrlPart + "/" + linkId);
        }

        private static RestLinkBuilder builder;
        private static RestLinks links;
        private static Url url;
        private static string linkId;
        private static string rootUrlPart;
        private static string linkName;
    }
}