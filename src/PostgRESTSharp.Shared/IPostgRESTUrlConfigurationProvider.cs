namespace PostgRESTSharp.Shared
{
    public interface IPostgRESTUrlConfigurationProvider
    {
        string Url { get; }
        //string BasicAuthorizationPassword { get; }
    }
}