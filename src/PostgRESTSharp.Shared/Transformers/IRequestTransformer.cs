using System.Collections.Generic;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IRequestTransformer
    {
        void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> queryString,
            IList<KeyValuePair<string, IEnumerable<string>>> requestHeaders);
    }
}