using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs
{
    public class when_manually_adding_a_link : WithFakes
    {
        Establish that = () =>
        {
            links = new RestLinks();
            link = new SimpleRestLink("http://localhosty/1");
            linkName = "linky1";
        };

        private Because of = () =>
        {
            links.AddLink(linkName, link);
        };

        private It should_have_one_link = () =>
        {
            links.Count.ShouldEqual(1);
        };

        private It should_be_the_expected_link = () =>
        {
            links.First().Key.ShouldEqual(linkName);
            links.First().Value.ShouldEqual(link);
        };

        private static RestLinks links;
        private static SimpleRestLink link;
        private static string linkName;
    }
}
