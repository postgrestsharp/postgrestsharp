using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.TransformerSpecs.RequestHeader.Range
{
    public class when_supplying_a_range_header : WithFakes
    {
        Establish that = () =>
        {
            rangeHeaderKey = "Range";
            rangeHeaderValue = "0-10";
            rangeUnitHeaderKey = "Range-Unit";
            rangeUnitHeaderValue = "items";
            request = new Request("GET", new Url("http://localhosty:1234/"),headers: new Dictionary<string, IEnumerable<string>>
            {
                { rangeHeaderKey, new[] { rangeHeaderValue } },
                { rangeUnitHeaderKey, new[] { rangeUnitHeaderValue } },
            });
            transformer = new RangeHeaderTransformer();
            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_have_copied_the_range_header = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals(rangeHeaderKey, StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { rangeHeaderValue });
        };

        private It should_have_copied_the_range_unit_header = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals(rangeUnitHeaderKey, StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { rangeUnitHeaderValue });
        };

        private It should_only_have_2_headers = () =>
        {
            transformedHeaders.Count.ShouldEqual(2);
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
