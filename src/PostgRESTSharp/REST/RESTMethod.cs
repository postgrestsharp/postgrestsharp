using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.REST
{
    public class RESTMethod
    {
        public RESTMethod(RESTVerbEnum verb, RESTVerbDetailEnum verbDetail, IEnumerable<RESTParameter> uriParameters, IEnumerable<RESTParameter> queryParameters)
        {
            this.Verb = verb;
            this.VerbDetail = verbDetail;
            this.URIParameters = new List<RESTParameter>(uriParameters);
            this.QueryParameters = new List<RESTParameter>(queryParameters);
            this.Description = "Temp Description";
        }

        public RESTVerbEnum Verb { get; protected set; }

        public RESTVerbDetailEnum VerbDetail { get; protected set; }

        public IEnumerable<RESTParameter> URIParameters { get; protected set; }

        public IEnumerable<RESTParameter> QueryParameters { get; protected set; }

        public string Description { get; protected set; }
    }
}
