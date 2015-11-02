using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.HttpStatusCodeMatcherSpecs
{
    public class when_checking_if_status_code_is_in_group_with_exact_multiple : WithFakes
    {
        Establish that = () =>
        {
            matcher = new HttpStatusCodeMatcher();
            statusCode = 100; //Continue
        };

        private Because of = () =>
        {
            result = matcher.IsInGroup(statusCode, 100);
        };

        private It should_be_true = () =>
        {
            result.ShouldBeTrue();
        };

        private static HttpStatusCodeMatcher matcher;
        private static bool result;
        private static int statusCode;
    }
}