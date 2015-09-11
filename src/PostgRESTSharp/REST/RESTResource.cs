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
            this.ResponseDefinitions = new List<RESTResponseDefinition>();
            this.RequestDefinition = new RESTRequestDefinition("", "");
            this.DisplayName = "Temp Display Name";
        }

        public string Uri { get; protected set; }

        public string PostgRESTUri { get; protected set; }

        public string ModelName { get; protected set; }

        public string KeyName { get; protected set; }

        public string DisplayName { get; protected set; }

        public IEnumerable<RESTMethod> Methods { get; protected set; }

        public RESTRequestDefinition RequestDefinition { get; protected set; }

        public IEnumerable<RESTResponseDefinition> ResponseDefinitions { get; protected set; }
    }
}