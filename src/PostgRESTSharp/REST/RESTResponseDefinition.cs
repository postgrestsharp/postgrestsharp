using System.Net;

namespace PostgRESTSharp.REST
{
    public class RESTResponseDefinition
    {
        public RESTResponseDefinition(HttpStatusCode statusCode, string responseSchema, string responseExample)
        {
            this.StatusCode = statusCode;
            this.ResponseSchema = responseSchema;
            this.ResponseExample = responseExample;
        }

        public HttpStatusCode StatusCode { get; protected set; }

        public string ResponseSchema { get; protected set; }

        public string ResponseExample { get; protected set; }
    }
}