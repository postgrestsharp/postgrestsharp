using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Machine.Fakes;
using Machine.Specifications;

namespace PostgRESTSharp.Shared.Specs.LocationHeaderParserSpecs
{
    public class when_parsing_a_location_header : WithFakes
    {
        Establish that = () =>
        {
            parser = new LocationHeaderParser();
        };

        private Because of = () =>
        {
            result = parser.ParseLocationHeader<int>("id", new Uri("http://localhosty/entity/1"));
        };

        private It should_return_the_default_for_the_type = () =>
        {
            //TODO: complete this parser to return the correct value
            result.ShouldEqual(default(int));
        };

        private static LocationHeaderParser parser;
        private static int result;
    }
}
