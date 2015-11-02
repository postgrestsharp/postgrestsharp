using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer
{
    public class when_requesting_empty_query_string_parameters : WithFakes
    {
        Establish that = () =>
        {
            transformers = new Shared.RequestTransformer(new List<IQueryStringTransformer>(), new List<IRequestHeaderTransformer>());
        };

        private Because of = () =>
        {
            headers = transformers.EmptyQuery();
        };

        private It should_provide_empty_query_string_parameters = () =>
        {
            headers.ShouldBeEmpty();
        };

        private static Shared.RequestTransformer transformers;
        private static IList<KeyValuePair<string, string>> headers;
    }
}