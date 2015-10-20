using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public class RESTResource : IRESTResource
    {
        public RESTResource(string uri, string postgRESTUri, string modelName, string keyName, 
            IEnumerable<RESTMethod> methods, IEnumerable<string> roleClaims, string resourceVersion,
            bool isExcluded)
        {
            this.Uri = uri;
            this.PostgRESTUri = postgRESTUri;
            this.ModelName = modelName;
            this.KeyName = keyName;
            this.Methods = new List<RESTMethod>(methods);
            this.AccessClaims = roleClaims;
            this.DisplayName = "Temp Resource Display Name";
            this.ResourceVersion = resourceVersion;
            this.IsExcluded = isExcluded;
        }

        public string Uri { get; protected set; }

        public IEnumerable<string> AccessClaims { get; protected set; }

        public string PostgRESTUri { get; protected set; }

        public string ModelName { get; protected set; }

        public string KeyName { get; protected set; }

        public string DisplayName { get; protected set; }

        public string ResourceVersion { get; protected set; }

        public bool IsExcluded { get; protected set; }

        public IEnumerable<RESTMethod> Methods { get; protected set; }
    }
}