using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_equating_status_codes_with_non_matching_codes : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            nancyStatusCode = Nancy.HttpStatusCode.OK;
            netStatusCode = System.Net.HttpStatusCode.BadGateway;
        };

        private Because of = () =>
        {
            result = matcher.AreEqual(nancyStatusCode, netStatusCode);
        };

        private It should_not_be_equal = () =>
        {
            result.ShouldBeFalse();
        };

        private static HttpStatusCodeMatcher matcher;
        private static System.Net.HttpStatusCode netStatusCode;
        private static Nancy.HttpStatusCode nancyStatusCode;
        private static bool result;
    }
}