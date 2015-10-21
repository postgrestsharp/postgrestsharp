using RestSharp;

namespace PostgRESTSharp.Shared
{
    public class ApiResponse : IApiResponse
    {
        public IRestResponse Response { get; protected set; }
    }

    public class ApiResponse<T> : ApiResponse, IApiResponse<T>
    {
        public T Data { get; private set; }

        public ApiResponse(IRestResponse response, T data)
        {
            Response = response;
            Data = data;
        }
    }
}