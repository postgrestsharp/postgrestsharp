namespace PostgRESTSharp.REST
{
    public class RESTModelProperty
    {
        public RESTModelProperty(string name, string description, string type)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Type { get; protected set; }
    }
}