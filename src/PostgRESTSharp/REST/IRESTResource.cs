using System.Collections.Generic;

namespace PostgRESTSharp.REST
{
    public interface IRESTResource
    {
        IEnumerable<RESTMethod> Methods { get; }

        IEnumerable<string> AccessClaims { get; } 

        string PostgRESTUri { get; }

        string Uri { get; }

        string DisplayName { get; }

        bool IsExcluded { get; }
    }
}