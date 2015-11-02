using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_server_error : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 501; //NotImplemented
            nancyStatusCode = (Nancy.HttpStatusCode)statusCode;
            netStatusCode = (System.Net.HttpStatusCode)statusCode;
        };

        private Because of = () =>
        {
            intResult = matcher.IsServerError(statusCode);
            nancyResult = matcher.IsServerError(nancyStatusCode);
            netResult = matcher.IsServerError(netStatusCode);
        };

        private It should_all_be_server_error = () =>
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