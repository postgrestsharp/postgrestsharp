using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestHeader.Version
{
    public class when_supplying_a_version_header : WithFakes
    {
        Establish that = () =>
        {
            headerKey = "X-Api-Version";
            headerValue = "1";
            request = new Request("GET", new Url("http://localhosty:1234/"), headers: new Dictionary<string, IEnumerable<string>>
            {
                { headerKey, new[] { headerValue } },
            });
            transformer = new VersionHeaderTransformer();
            transformedHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, transformedHeaders);
        };

        private It should_have_copied_the_range_header_to_the_accept_header = () =>
        {
            transformedHeaders
                .Single(a => a.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldBeLike(new[] { "application/json; version=" + headerValue });
        };

        private It should_only_have_2_headers = () =>
        {
            transformedHeaders.Count.ShouldEqual(1);
        };

        private static string headerKey;
        private static string headerValue;
        private static Request request;
        private static VersionHeaderTransformer transformer;
        private static List<KeyValuePair<string, IEnumerable<string>>> transformedHeaders;
    }
}
