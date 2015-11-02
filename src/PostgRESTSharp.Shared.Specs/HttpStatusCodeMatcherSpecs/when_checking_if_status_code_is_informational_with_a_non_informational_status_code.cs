using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_informational_with_a_non_informational_status_code : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 201; //Created
            nancyStatusCode = (Nancy.HttpStatusCode)statusCode;
            netStatusCode = (System.Net.HttpStatusCode)statusCode;
        };

        private Because of = () =>
        {
            intResult = matcher.IsInformational(statusCode);
            nancyResult = matcher.IsInformational(nancyStatusCode);
            netResult = matcher.IsInformational(netStatusCode);
        };

        private It should_all_not_be_informational = () =>
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