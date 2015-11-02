using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace PostgRESTSharp.Shared
{
    [Order(2)]
    public class OrderByQueryStringTransformer : IQueryStringTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> postgRestQueryStringValuesToAddTo)
        {
            var requestOrderByValue = (string)incomingRequestToProcess.Query.order;
            if (string.IsNullOrWhiteSpace(requestOrderByValue))
            {
                return;
            }
            const string key = "order";
            var existingOrderBy = postgRestQueryStringValuesToAddTo.FirstOrDefault(a => a.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (existingOrderBy.Key != null)
            {
                //remove existing order by before applying the one the client has requested
                postgRestQueryStringValuesToAddTo.Remove(existingOrderBy);
            }
            postgRestQueryStringValuesToAddTo.Add(new KeyValuePair<string, string>(key, requestOrderByValue));
        }
    }
}
