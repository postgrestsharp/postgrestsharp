using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public class RESTMethod
    {
        public RESTMethod(RESTVerbEnum verb, RESTVerbDetailEnum verbDetail, IEnumerable<RESTParameter> uriParameters, IEnumerable<RESTParameter> queryParameters, 
            IEnumerable<RESTResponseDefinition> responseDefinitions, string methodDescription)
            : this(verb, verbDetail, uriParameters, queryParameters, methodDescription)
        {
            this.ResponseDefinitions = new List<RESTResponseDefinition>(responseDefinitions);
        }
        public RESTMethod(RESTVerbEnum verb, RESTVerbDetailEnum verbDetail, IEnumerable<RESTParameter> uriParameters, IEnumerable<RESTParameter> queryParameters, string methodDescription)
        {
            this.Verb = verb;
            this.VerbDetail = verbDetail;
            this.URIParameters = new List<RESTParameter>(uriParameters);
            this.QueryParameters = new List<RESTParameter>(queryParameters);
            this.Description = methodDescription;
            this.ResponseDefinitions = new List<RESTResponseDefinition>();

            //{ 
            //    new RESTResponseDefinition(System.Net.HttpStatusCode.Accepted,"outputSchema","{\r\n\t'output1':'output',\r\n\t'output2':2}")
            //};
            this.RequestDefinition = new RESTRequestDefinition("schema", "{\r\n\t'input1':'input',\r\n\t'input2':1}");
        }

        public RESTVerbEnum Verb { get; protected set; }

        public RESTVerbDetailEnum VerbDetail { get; protected set; }

        public IEnumerable<RESTParameter> URIParameters { get; protected set; }

        public IEnumerable<RESTParameter> QueryParameters { get; protected set; }

        public string Description { get; protected set; }

        public RESTRequestDefinition RequestDefinition { get; protected set; }

        public IEnumerable<RESTResponseDefinition> ResponseDefinitions { get; protected set; }
    }
}