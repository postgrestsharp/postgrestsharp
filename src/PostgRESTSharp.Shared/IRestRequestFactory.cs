using RestSharp;

namespace PostgRESTSharp.Shared
{
    public interface IRestRequestFactory
    {
        IRestRequest Create();
    }
}