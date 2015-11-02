using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.TransformerSpecs.RequestTransformer
{
    public class when_requesting_empty_headers : WithFakes
    {
        Establish that = () =>
        {
            transformers = new Shared.RequestTransformer(new List<IQueryStringTransformer>(), new List<IRequestHeaderTransformer>());
        };

        private Because of = () =>
        {
            headers = transformers.EmptyHeaders();
        };

        private It should_provide_empty_headers = () =>
        {
            headers.ShouldBeEmpty();
        };

        private static Shared.RequestTransformer transformers;
        private static IList<KeyValuePair<string, IEnumerable<string>>> headers;
    }
}