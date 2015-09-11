using PostgRESTSharp.REST;

namespace PostgRESTSharp
{
    public interface IRESTModelBuilder
    {
        RESTModel BuildRESTModel(IViewMetaModel view, RESTVerbEnum modelType);
    }
}