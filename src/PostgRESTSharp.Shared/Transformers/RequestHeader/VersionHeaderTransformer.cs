using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public class VersionHeaderTransformer : IRequestHeaderTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo)
        {
            var versionHeader = incomingRequestToProcess.Headers.SingleOrDefault(a => a.Key.Equals("X-Api-Version", StringComparison.OrdinalIgnoreCase));

            var acceptHeaderValue = versionHeader.Key == null
                ? "application/json;"
                : "application/json; version=" + versionHeader.Value.FirstOrDefault();

            postgRestHeadersToAddTo.Add(new KeyValuePair<string, IEnumerable<string>>("Accept", new[] { acceptHeaderValue }));
        }
    }
}
