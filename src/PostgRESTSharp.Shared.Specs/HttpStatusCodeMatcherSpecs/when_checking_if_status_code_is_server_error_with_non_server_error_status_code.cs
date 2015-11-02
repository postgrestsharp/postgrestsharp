using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_server_error_with_non_server_error_status_code : WithFakes
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
            intResult = matcher.IsServerError(statusCode);
            nancyResult = matcher.IsServerError(nancyStatusCode);
            netResult = matcher.IsServerError(netStatusCode);
        };

        private It should_all_not_be_server_error = () =>
        {
            intResult.ShouldBeFalse();
            nancyResult.ShouldBeFalse();
            netResult.ShouldBeFalse();
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