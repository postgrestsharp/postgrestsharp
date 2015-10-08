using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public class PostgRestRangeHeaderTransformer : IRequestHeaderTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo)
        {
            var rangeHeader = incomingRequestToProcess.Headers.SingleOrDefault(a => a.Key.Equals("Range", StringComparison.OrdinalIgnoreCase));
            var rangeUnitHeader = incomingRequestToProcess.Headers.SingleOrDefault(a => a.Key.Equals("Range-Unit", StringComparison.OrdinalIgnoreCase));

            if ((rangeHeader.Key == null) == (rangeUnitHeader.Key == null)) //XNOR
            {
                //has both range + range header, or neither
                if (rangeHeader.Key != null) //only need to check one because of XNOR
                {
                    postgRestHeadersToAddTo.Add(new KeyValuePair<string, IEnumerable<string>>(rangeUnitHeader.Key, rangeUnitHeader.Value));
                    postgRestHeadersToAddTo.Add(new KeyValuePair<string, IEnumerable<string>>(rangeHeader.Key, rangeHeader.Value));
                }
            }
        }
    }
}
