using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_informational : WithFakes
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
            intResult = matcher.IsInformational(statusCode);
            nancyResult = matcher.IsInformational(nancyStatusCode);
            netResult = matcher.IsInformational(netStatusCode);
        };

        private It should_all_be_informational = () =>
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