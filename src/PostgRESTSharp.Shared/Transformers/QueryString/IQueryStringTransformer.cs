using System.Collections.Generic;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IQueryStringTransformer
    {
        void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> postgRestQueryStringValuesToAddTo);
    }
}
