using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace PostgRESTSharp.Shared
{
    [Order(1)]
    public class PassThroughQueryStringTransformer : IQueryStringTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> postgRestQueryStringValuesToAddTo)
        {
            var query = (IEnumerable<string>)incomingRequestToProcess.Query;

            foreach (var key in query)
            {
                var value = incomingRequestToProcess.Query[key] as object;
                postgRestQueryStringValuesToAddTo.Add(new KeyValuePair<string, string>(key, (value ?? string.Empty).ToString()));
            }
        }
    }
}
