using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_converting_a_system_net_status_code_to_nancy_status_code : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            netStatusCode = System.Net.HttpStatusCode.OK;
        };

        private Because of = () =>
        {
            nancyStatusCode = matcher.ToNancy(netStatusCode);
        };

        private It should_be_the_equivalent_of_the_net_http_status_code = () =>
        {
            ((int)nancyStatusCode).ShouldEqual((int)netStatusCode);
        };

        private static HttpStatusCodeMatcher matcher;
        private static System.Net.HttpStatusCode netStatusCode;
        private static Nancy.HttpStatusCode nancyStatusCode;
    }
}
