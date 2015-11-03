using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Machine.Fakes;
using Machine.Specifications;
using Nancy.Json;
using Newtonsoft.Json;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.JavaScriptConverter.Deserialise
{
    public class when_deserialising : WithFakes
    {
        Establish that = () =>
        {
            converter = new RestLinksJavaScriptConverter();
        };

        Because of = () =>
        {
            result = converter.Deserialize(null, typeof(object), new JavaScriptSerializer());
        };

        It should_not_have_returned_a_result = () =>
        {
            result.ShouldBeNull();
        };

        private static RestLinksJavaScriptConverter converter;
        private static object result;
    }
}
