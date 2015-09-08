using PostgRESTSharp.REST;
using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IRESTResourceProcessor
    {
        IEnumerable<RESTResource> Process(IEnumerable<IViewMetaModel> views, bool isReadOnly);
    }
}