using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public class RESTResource : IRESTResource
    {
        public RESTResource(string uri, string postgRESTUri , string modelName, string keyName, IEnumerable<RESTMethod> methods)
        {
            this.Uri = uri;
            this.PostgRESTUri = postgRESTUri;
            this.ModelName = modelName;
            this.KeyName = keyName;
            this.Methods = new List<RESTMethod>(methods);
        }

        public string Uri { get; protected set; }

        public string PostgRESTUri { get; protected set; }

        public string ModelName { get; protected set; }

        public string KeyName { get; protected set; }

        public IEnumerable<RESTMethod> Methods { get; protected set; }
    }
}