using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_equating_status_codes : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            nancyStatusCode = Nancy.HttpStatusCode.OK;
            netStatusCode = System.Net.HttpStatusCode.OK;
        };

        private Because of = () =>
        {
            result = matcher.AreEqual(nancyStatusCode, netStatusCode);
        };

        private It should_be_equal = () =>
        {
            result.ShouldBeTrue();
        };

        private static HttpStatusCodeMatcher matcher;
        private static System.Net.HttpStatusCode netStatusCode;
        private static Nancy.HttpStatusCode nancyStatusCode;
        private static bool result;
    }
}