using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.TransformerSpecs.QueryString.PassThrough
{
    public class when_supplying_a_query_string : WithFakes
    {
        Establish that = () =>
        {
            request = new Request("GET", new Url("http://localhosty:1234?a=1&b=2&c=3"));
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
                .ShouldEqual("1");
        };

        private It should_have_copied_the_second_value = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("b", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual("2");
        };

        private It should_have_copied_the_third_value = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("c", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual("3");
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
