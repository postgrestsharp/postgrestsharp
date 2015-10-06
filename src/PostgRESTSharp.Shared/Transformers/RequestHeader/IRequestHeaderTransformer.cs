using System.Collections.Generic;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IRequestHeaderTransformer
    {
        void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo);
    }
}
