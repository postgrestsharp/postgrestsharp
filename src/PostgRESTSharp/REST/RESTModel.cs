using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public class RESTModel
    {
        public RESTModel(RESTVerbEnum modelType, string name, string modelClassName, string description, string primaryKeyPropertyName, IEnumerable<RESTModelProperty> properties, IEnumerable<RESTModelProperty> parameters)
        {
            this.ModelType = modelType;
            this.Name = name;
            this.ModelClassName = modelClassName;
            this.Description = description;
            this.PrimaryKeyPropertyName = primaryKeyPropertyName;
            this.Properties = new List<RESTModelProperty>(properties);
            this.Parameters = new List<RESTModelProperty>(parameters);
        }

        public RESTVerbEnum ModelType { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string ModelClassName { get; protected set; }

        public string PrimaryKeyPropertyName { get; protected set; }

        public IEnumerable<RESTModelProperty> Properties { get; protected set; }

        public IEnumerable<RESTModelProperty> Parameters { get; protected set; }
    }
}