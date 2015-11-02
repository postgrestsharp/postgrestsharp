using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer
{
    public class when_transforming_an_incoming_request : WithFakes
    {
        Establish that = () =>
        {
            queryStringTransformers = new List<IQueryStringTransformer>
            {
                An<IQueryStringTransformer>(),
                An<IQueryStringTransformer>(),
            };
            requestHeaderTransformers = new List<IRequestHeaderTransformer>
            {
                An<IRequestHeaderTransformer>(),
                An<IRequestHeaderTransformer>(),
            };
            transformers = new Shared.RequestTransformer(queryStringTransformers, requestHeaderTransformers);

            request = new Request("GET", new Url("http://localhosty:1234"));

            transformedQueryStringParameters = new List<KeyValuePair<string, string>>();
            transformedRequestHeaders = new List<KeyValuePair<string, IEnumerable<string>>>();
        };

        private Because of = () =>
        {
            transformers.Transform(request, transformedQueryStringParameters, transformedRequestHeaders);
        };

        private It should_have_called_transform_on_each_query_string_transformer = () =>
        {
            foreach (var item in queryStringTransformers)
            {
                item.WasToldTo(a => a.Transform(request, transformedQueryStringParameters)).OnlyOnce();
            }
        };

        private It should_have_called_transform_on_each_request_header_transformer = () =>
        {
            foreach (var item in requestHeaderTransformers)
            {
                item.WasToldTo(a => a.Transform(request, transformedRequestHeaders)).OnlyOnce();
            }
        };

        private static List<IQueryStringTransformer> queryStringTransformers;
        private static Shared.RequestTransformer transformers;
        private static List<IRequestHeaderTransformer> requestHeaderTransformers;
        private static Request request;
        private static IList<KeyValuePair<string, string>> transformedQueryStringParameters;
        private static IList<KeyValuePair<string, IEnumerable<string>>> transformedRequestHeaders;
    }
}
