using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.QueryString.PassThrough
{
    public class when_supplying_a_query_string_with_missing_values : WithFakes
    {
        Establish that = () =>
        {
            request = new Request("GET", new Url("http://localhosty:1234?a=&b=&c="));
            transformer = new PassThroughQueryStringTransformer();
            queryStringParameters = new List<KeyValuePair<string, string>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, queryStringParameters);
        };

        private It should_have_copied_the_first_value = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("a", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual(string.Empty);
        };

        private It should_have_copied_the_second_value = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("b", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual(string.Empty);
        };

        private It should_have_copied_the_third_value = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("c", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual(string.Empty);
        };

        private It should_only_have_3_parameters = () =>
        {
            queryStringParameters.Count.ShouldEqual(3);
        };

        private static string headerKey;
        private static string headerValue;
        private static Request request;
        private static PassThroughQueryStringTransformer transformer;
        private static List<KeyValuePair<string, string>> queryStringParameters;
    }
}