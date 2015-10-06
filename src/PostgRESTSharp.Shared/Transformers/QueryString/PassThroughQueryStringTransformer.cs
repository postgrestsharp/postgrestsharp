using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public class PassThroughQueryStringTransformer : IQueryStringTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> postgRestQueryStringValuesToAddTo)
        {
            if (postgRestQueryStringValuesToAddTo == null)
            {
                postgRestQueryStringValuesToAddTo = new List<KeyValuePair<string, string>>();
            }

            var query = (IEnumerable<string>)incomingRequestToProcess.Query;

            foreach (var key in query)
            {
                var value = (string)incomingRequestToProcess.Query[key];
                postgRestQueryStringValuesToAddTo.Add(new KeyValuePair<string, string>(key, value));
            }
        }
    }
}
