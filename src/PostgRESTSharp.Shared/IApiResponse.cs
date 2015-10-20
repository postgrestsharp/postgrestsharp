using RestSharp;

namespace PostgRESTSharp.Shared
{
    public interface IApiResponse
    {
        IRestResponse Response { get; }
    }

    public interface IApiResponse<out T> : IApiResponse
    {
        T Data { get; }
    }
}