﻿using System.Collections.Generic;
using Nancy;

namespace PostgRESTSharp.Shared
{
    public class RequestTransformer : IRequestTransformer
    {
        private readonly IEnumerable<IQueryStringTransformer> queryStringTransformers;
        private readonly IEnumerable<IRequestHeaderTransformer> requestHeaderTransformers;

        public RequestTransformer(IEnumerable<IQueryStringTransformer> queryStringTransformers, IEnumerable<IRequestHeaderTransformer> requestHeaderTransformers)
        {
            this.queryStringTransformers = queryStringTransformers;
            this.requestHeaderTransformers = requestHeaderTransformers;
        }

        public IList<KeyValuePair<string, string>> EmptyQuery()
        {
            return new List<KeyValuePair<string, string>>();
        }

        public IList<KeyValuePair<string, IEnumerable<string>>> EmptyHeaders()
        {
            return new List<KeyValuePair<string, IEnumerable<string>>>();
        }

        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, string>> queryString, IList<KeyValuePair<string, IEnumerable<string>>> requestHeaders)
        {
            foreach (var item in this.requestHeaderTransformers.OrderByOrderAttribute())
            {
                item.Transform(incomingRequestToProcess, requestHeaders);
            }

            foreach (var item in this.queryStringTransformers.OrderByOrderAttribute())
            {
                item.Transform(incomingRequestToProcess, queryString);
            }
        }
    }
}
