using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.Builder
{
    public class when_building_a_self_link : WithFakes
    {
        Establish that = () =>
        {
            builder = new RestLinkBuilder();
            links = new RestLinks();
            url = new Url("http://somewhere.com/");
        };

        private Because of = () =>
        {
            builder.AddSelfLink(links, url);
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
            return link != null && link.Uri.Href.Equals(url);
        }

        private static RestLinkBuilder builder;
        private static RestLinks links;
        private static Url url;
    }
}
