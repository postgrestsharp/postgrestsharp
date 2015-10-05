namespace PostgRESTSharp.Shared
{
    public interface IHttpStatusCodeMatcher
    {
        Nancy.HttpStatusCode ToNancy(System.Net.HttpStatusCode systemNetHttpStatusCode);
        System.Net.HttpStatusCode ToSystemNet(Nancy.HttpStatusCode nancyHttpStatusCode);
        bool AreEqual(System.Net.HttpStatusCode systemNetHttpStatusCode, Nancy.HttpStatusCode nancyHttpStatusCode);
        bool AreEqual(Nancy.HttpStatusCode nancyHttpStatusCode, System.Net.HttpStatusCode systemNetHttpStatusCode);
        bool IsInGroup(int httpStatusCode, int group);
        bool IsInformational(int httpStatusCode);
        bool IsSuccess(int httpStatusCode);
        bool IsRedirection(int httpStatusCode);
        bool IsClientError(int httpStatusCode);
        bool IsServerError(int httpStatusCode);
        bool IsInformational(Nancy.HttpStatusCode httpStatusCode);
        bool IsInformational(System.Net.HttpStatusCode httpStatusCode);
        bool IsSuccess(Nancy.HttpStatusCode httpStatusCode);
        bool IsSuccess(System.Net.HttpStatusCode httpStatusCode);
        bool IsRedirection(Nancy.HttpStatusCode httpStatusCode);
        bool IsRedirection(System.Net.HttpStatusCode httpStatusCode);
        bool IsClientError(Nancy.HttpStatusCode httpStatusCode);
        bool IsClientError(System.Net.HttpStatusCode httpStatusCode);
        bool IsServerError(Nancy.HttpStatusCode httpStatusCode);
        bool IsServerError(System.Net.HttpStatusCode httpStatusCode);
    }
}