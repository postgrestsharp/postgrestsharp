using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.QueryString.PassThrough
{
    public class when_not_supplying_a_query_string : WithFakes
    {
        Establish that = () =>
        {
            request = new Request("GET", new Url("http://localhosty:1234"));
            transformer = new PassThroughQueryStringTransformer();
            queryStringParameters = new List<KeyValuePair<string, string>>();
        };

        private Because of = () =>
        {
            transformer.Transform(request, queryStringParameters);
        };

        private It should_only_have_no_parameters = () =>
        {
            queryStringParameters.ShouldBeEmpty();
        };

        private static Request request;
        private static PassThroughQueryStringTransformer transformer;
        private static List<KeyValuePair<string, string>> queryStringParameters;
    }
}