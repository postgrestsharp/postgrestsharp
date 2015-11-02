using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.TransformerSpecs.RequestHeader.Range
{
    public class when_supplying_a_range_header_with_no_range : WithFakes
    {
        Establish that = () =>
        {
            rangeHeaderKey = "Range";
            rangeHeaderValue = "0-10";
            rangeUnitHeaderKey = "Range-Unit";
            rangeUnitHeaderValue = "items";
            request = new Request("GET", new Url("http://localhosty:1234/"),headers: new Dictionary<string, IEnumerable<string>>
            {
                //{ rangeHeaderKey, new[] { rangeHeaderValue } },
                { rangeUnitHeaderKey, new[] { rangeUnitHeaderValue } },
            });
            transformer = new RangeHeaderTransformer();
            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_not_have_copied_the_range_header = () =>
        {
            transformedHeaders.ShouldNotContain(a => a.Key.Equals(rangeHeaderKey, StringComparison.OrdinalIgnoreCase));
        };

        private It should_not_have_copied_the_range_unit_header = () =>
        {
            transformedHeaders.ShouldNotContain(a => a.Key.Equals(rangeUnitHeaderKey, StringComparison.OrdinalIgnoreCase));
        };

        private It should_have_no_headers = () =>
        {
            transformedHeaders.Count.ShouldEqual(0);
        };

        private static string rangeHeaderKey;
        private static string rangeHeaderValue;
        private static string rangeUnitHeaderKey;
        private static string rangeUnitHeaderValue;
        private static Request request;
        private static RangeHeaderTransformer transformer;
        private static List<KeyValuePair<string, IEnumerable<string>>> transformedHeaders;
    }
}