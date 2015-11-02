using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer
{
    public class when_transforming_an_incoming_request_with_ordered_transformers : WithFakes
    {
        Establish that = () =>
        {
            queryStringTransformers = An<List<IQueryStringTransformer>>();
            requestHeaderTransformers = An<List<IRequestHeaderTransformer>>();
            transformers = new Shared.RequestTransformer(queryStringTransformers, requestHeaderTransformers);

            request = new Request("GET", new Url("http://localhosty:1234"));

            transformedQueryStringParameters = new List<KeyValuePair<string, string>>();
            transformedRequestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformers.Transform(request, transformedQueryStringParameters, transformedRequestHeaders);
        };

        private It should_have_ordered_the_query_string_transformers = () =>
        {
            queryStringTransformers.WasToldTo(a => a.OrderByOrderAttribute()).OnlyOnce();
        };

        private It should_have_ordered_the_request_header_transformers = () =>
        {
            requestHeaderTransformers.WasToldTo(a => a.OrderByOrderAttribute()).OnlyOnce();
        };

        private static List<IQueryStringTransformer> queryStringTransformers;
        private static Shared.RequestTransformer transformers;
        private static List<IRequestHeaderTransformer> requestHeaderTransformers;
        private static Request request;
        private static IList<KeyValuePair<string, string>> transformedQueryStringParameters;
        private static IList<KeyValuePair<string, IEnumerable<string>>> transformedRequestHeaders;
    }
}