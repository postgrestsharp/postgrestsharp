using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.Transformers.RequestHeader
{
    public class when_transforming_a_range_header_with_an_acceptable_range : WithFakes
    {
        Establish that = () =>
        {
            rangeHeaderKey = "Range";
            rangeHeaderValue = "0-10";
            rangeUnitHeaderKey = "Range-Unit";
            rangeUnitHeaderValue = "items";
            request = new Request("GET", new Url("http://localhosty:1234/"));
            transformer = new PostgRestRangeLimitHeaderTransformer();
            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                new KeyValuePair<string, IEnumerable<string>>(rangeHeaderKey, new[] { rangeHeaderValue }),
                new KeyValuePair<string, IEnumerable<string>>(rangeUnitHeaderKey, new[] { rangeUnitHeaderValue }),
            };
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_accept_the_provided_range = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals(rangeHeaderKey, StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { rangeHeaderValue });
        };

        private It should_have_inserted_a_range_unit_header = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals(rangeUnitHeaderKey, StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { rangeUnitHeaderValue });
        };

        private static Request request;
        private static PostgRestRangeLimitHeaderTransformer transformer;
        private static List<KeyValuePair<string, IEnumerable<string>>> transformedHeaders;
        private static string rangeHeaderKey;
        private static string rangeUnitHeaderKey;
        private static string rangeHeaderValue;
        private static string rangeUnitHeaderValue;
    }
}
