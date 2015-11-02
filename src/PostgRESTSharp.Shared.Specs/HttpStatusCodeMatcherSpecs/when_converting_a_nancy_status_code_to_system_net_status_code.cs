using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_converting_a_nancy_status_code_to_system_net_status_code : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            nancyStatusCode = Nancy.HttpStatusCode.OK;
        };

        private Because of = () =>
        {
            netStatusCode = matcher.ToSystemNet(nancyStatusCode);
        };

        private It should_be_the_equivalent_of_the_nancy_http_status_code = () =>
        {
            ((int)netStatusCode).ShouldEqual((int)nancyStatusCode);
        };

        private static HttpStatusCodeMatcher matcher;
        private static System.Net.HttpStatusCode netStatusCode;
        private static Nancy.HttpStatusCode nancyStatusCode;
    }
}