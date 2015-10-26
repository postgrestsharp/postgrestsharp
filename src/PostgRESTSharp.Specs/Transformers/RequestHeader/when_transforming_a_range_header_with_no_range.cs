using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.Transformers.RequestHeader
{
    public class when_transforming_a_range_header_with_no_range : WithFakes
    {
        Establish that = () =>
        {
            rangeHeaderKey = "Range";
            rangeHeaderValue = "0-99";
            rangeUnitHeaderKey = "Range-Unit";
            rangeUnitHeaderValue = "items";
            request = new Request("GET", new Url("http://localhosty:1234/"));
            transformer = new PostgRestRangeLimitHeaderTransformer();

            //deliberately not provide any headers
            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_insert_a_default_range = () =>
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