namespace PostgRESTSharp.REST
{
    public class RESTRequestDefinition
    {
        public RESTRequestDefinition(string requestSchema, string requestExample)
        {
            this.RequestSchema = requestSchema;
            this.RequestExample = requestExample;
        }

        public string RequestSchema { get; protected set; }

        public string RequestExample { get; protected set; }
    }
}