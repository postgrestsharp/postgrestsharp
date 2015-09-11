namespace PostgRESTSharp.REST
{
    public class RESTModelProperty
    {
        public RESTModelProperty(string name, string description, string type)
        {
            this.Name = name;
            this.Type = ConvertType(type);
            this.Description = description;
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Type { get; protected set; }

        private string ConvertType(string type)
        {
            if(type.StartsWith("int"))
            {
                return "integer";
            }
            if(type.StartsWith("decimal"))
            {
                return "number";
            }

            return type;
        }
    }
}