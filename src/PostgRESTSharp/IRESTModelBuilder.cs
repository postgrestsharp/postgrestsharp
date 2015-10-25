using System.Collections.Generic;
using PostgRESTSharp.REST;

namespace PostgRESTSharp
{
    public interface IRESTModelBuilder
    {
        RESTModel BuildRESTModel(IViewMetaModel view, RESTVerbEnum modelType, IEnumerable<IViewMetaModel> views);
    }
}