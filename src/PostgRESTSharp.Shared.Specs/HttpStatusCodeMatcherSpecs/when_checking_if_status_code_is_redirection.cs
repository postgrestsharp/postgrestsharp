using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_redirection : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 300; //MultipleChoices
            nancyStatusCode = (Nancy.HttpStatusCode)statusCode;
            netStatusCode = (System.Net.HttpStatusCode)statusCode;
        };

        private Because of = () =>
        {
            intResult = matcher.IsRedirection(statusCode);
            nancyResult = matcher.IsRedirection(nancyStatusCode);
            netResult = matcher.IsRedirection(netStatusCode);
        };

        private It should_all_be_redirection = () =>
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