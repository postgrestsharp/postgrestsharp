using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public interface IRESTModelProperty
    {
        string Name { get; }

        string Description { get; }

        string Type { get; }

        IEnumerable<IRESTModelProperty> Properties { get; }

    }
}