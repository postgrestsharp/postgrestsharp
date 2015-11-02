using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_checking_if_serialiser_can_convert_an_IRestLink : WithFakes
    {
        Establish that = () =>
        {
            converter = new RestLinkJsonConverter();
        };

        private Because of = () =>
        {
            result = converter.CanConvert(typeof(IRestLink));
        };

        private It should_be_able_to_convert = () =>
        {
            result.ShouldBeTrue();
        };

        private static RestLinkJsonConverter converter;
        private static bool result;
    }
}
