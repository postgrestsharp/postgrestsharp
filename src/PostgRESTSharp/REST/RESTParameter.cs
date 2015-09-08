namespace PostgRESTSharp.REST
{
    public class RESTParameter
    {
        public RESTParameter(string name, string type, bool isRequired)
        {
            this.Name = name;
            this.Type = type;
            this.IsRequired = isRequired;
        }

        public string Name { get; protected set; }

        public string Type { get; protected set; }

        public bool IsRequired { get; protected set; }
    }
}