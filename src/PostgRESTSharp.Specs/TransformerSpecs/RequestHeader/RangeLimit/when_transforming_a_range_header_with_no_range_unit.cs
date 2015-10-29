using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.TransformerSpecs.RequestHeader.RangeLimit
{
    public class when_transforming_a_range_header_with_no_range_unit : WithFakes
    {
        Establish that = () =>
        {
            rangeHeaderKey = "Range";
            rangeHeaderValue = "0-99";
            rangeUnitHeaderKey = "Range-Unit";
            rangeUnitHeaderValue = "items";
            request = new Request("GET", new Url("http://localhosty:1234/"));
            transformer = new RangeLimitHeaderTransformer();

            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>
            {
                //deliberately do not provide range-unit header
                new KeyValuePair<string, IEnumerable<string>>(rangeHeaderKey, new[] { rangeHeaderValue }),
            };
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_have_inserted_a_range_unit_header = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals(rangeUnitHeaderKey, StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { rangeUnitHeaderValue });
        };

        private static Request request;
        private static RangeLimitHeaderTransformer transformer;
        private static List<KeyValuePair<string, IEnumerable<string>>> transformedHeaders;
        private static string rangeHeaderKey;
        private static string rangeUnitHeaderKey;
        private static string rangeHeaderValue;
        private static string rangeUnitHeaderValue;
    }
}