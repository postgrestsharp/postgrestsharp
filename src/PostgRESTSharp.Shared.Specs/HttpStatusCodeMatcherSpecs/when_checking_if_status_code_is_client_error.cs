using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_client_error : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 401; //Unauthorized
            nancyStatusCode = (Nancy.HttpStatusCode)statusCode;
            netStatusCode = (System.Net.HttpStatusCode)statusCode;
        };

        private Because of = () =>
        {
            intResult = matcher.IsClientError(statusCode);
            nancyResult = matcher.IsClientError(nancyStatusCode);
            netResult = matcher.IsClientError(netStatusCode);
        };

        private It should_all_be_client_error = () =>
        {
            intResult.ShouldBeTrue();
            nancyResult.ShouldBeTrue();
            netResult.ShouldBeTrue();
        };

        private static HttpStatusCodeMatcher matcher;
        private static System.Net.HttpStatusCode netStatusCode;
        private static Nancy.HttpStatusCode nancyStatusCode;
        private static bool intResult;
        private static bool nancyResult;
        private static bool netResult;
        private static int statusCode;
    }
}