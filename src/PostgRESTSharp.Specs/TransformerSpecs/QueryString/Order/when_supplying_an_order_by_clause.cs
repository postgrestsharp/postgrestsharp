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
    public class when_supplying_an_order_by_clause : WithFakes
    {
        Establish that = () =>
        {
            request = new Request("GET", new Url("http://localhosty:1234?order=id.desc"));
            transformer = new OrderByQueryStringTransformer();
            queryStringParameters = new List<KeyValuePair<string, string>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, queryStringParameters);
        };

        private It should_have_copied_the_order_by_clause = () =>
        {
            queryStringParameters
                .Single(a => a.Key.Equals("order", StringComparison.OrdinalIgnoreCase))
                .Value
                .ShouldEqual("id.desc");
        };

        private It should_only_have_1_parameter = () =>
        {
            queryStringParameters.Count.ShouldEqual(1);
        };

        private static string headerKey;
        private static string headerValue;
        private static Request request;
        private static OrderByQueryStringTransformer transformer;
        private static List<KeyValuePair<string, string>> queryStringParameters;
    }
}
