using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.JavaScriptConverter
{
    public class when_determining_the_supported_types : WithFakes
    {
        Establish that = () =>
        {
            converter = new RestLinksJavaScriptConverter();
        };

        private Because of = () =>
        {
        };

        private It should_have_at_least_one_supported_type = () =>
        {
            converter.SupportedTypes.ShouldNotBeEmpty();
        };

        private It should_have_rest_links_as_a_supported_type = () =>
        {
            converter.SupportedTypes.ShouldContain(typeof(IRestLink));
        };

        private static RestLinksJavaScriptConverter converter;
    }
}
