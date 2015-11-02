using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_success_with_a_non_success_status_code : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 101; //SwitchingProtocols
            nancyStatusCode = (Nancy.HttpStatusCode)statusCode;
            netStatusCode = (System.Net.HttpStatusCode)statusCode;
        };

        private Because of = () =>
        {
            intResult = matcher.IsSuccess(statusCode);
            nancyResult = matcher.IsSuccess(nancyStatusCode);
            netResult = matcher.IsSuccess(netStatusCode);
        };

        private It should_all_not_be_success = () =>
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