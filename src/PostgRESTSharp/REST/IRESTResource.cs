using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public interface IRESTResource
    {
        IEnumerable<RESTMethod> Methods { get; }

        string PostgRESTUri { get; }

        string Uri { get; }

        RESTRequestDefinition RequestDefinition { get; }

        IEnumerable<RESTResponseDefinition> ResponseDefinitions { get; }
    }
}