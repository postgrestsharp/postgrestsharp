using PostgRESTSharp.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML.Transitions
{
    public class RAMLNestedResource
    {
        public RAMLNestedResource(RESTVerbEnum verb, RESTVerbDetailEnum verbDetail, RESTMethod method,RESTParameter uriParameter)
        {
            this.Verb = verb;
            this.VerbDetail = verbDetail;
            this.URIParameter = uriParameter;
            this.Method = method;
        }

        public RESTVerbEnum Verb { get; protected set; }

        public RESTVerbDetailEnum VerbDetail { get; protected set; }

        public RESTParameter URIParameter { get; protected set; }

        public RESTMethod Method { get; protected set; }

    }
}
