using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace PostgRESTSharp.Shared.Transformers.QueryString
{
    [Order(2)]
    public class OrderByQueryStringTransformer : IQueryStringTransformer
    {
        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> postgRestQueryStringValuesToAddTo)
        {
            var query = incomingRequestToProcess.Query;
            if (query == null)
            {
                return;
            }
            var requestOrderByValue = query.order;
            if (requestOrderByValue == null)
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
