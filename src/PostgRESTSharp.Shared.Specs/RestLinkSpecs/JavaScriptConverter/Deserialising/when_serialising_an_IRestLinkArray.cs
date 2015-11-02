using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy.Json;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;
using PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Mock;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JavaScriptConverter.Deserialising
{
    public class when_serialising_an_IRestLinkArray : WithFakes
    {
        Establish that = () =>
        {
            link = new RestArrayLink();
            link.Uris = new IRestLinkUri[1];
            link.Uris[0] = new RestLinkUri("http://localhosty/1");

            converter = new RestLinksJavaScriptConverter();
        };

        private Because of = () =>
        {
            values = converter.Serialize(link, new JavaScriptSerializer());
            json = JsonConvert.SerializeObject(link, Formatting.Indented);
        };

        private It should_not_have_serialised_the_object = () =>
        {
            values.Count.ShouldEqual(0);
        };

        private It should_have_similar_values_to_the_newtonsoft_JsonConverter_result = () =>
        {
            //values is empty, so we can assume it is equivalent to an empty string for the sake of json serialisation
            json.ShouldEqual(string.Empty);
        };

        private static RestArrayLink link;
        private static string json;
        private static dynamic jsonObject;
        private static RestLinksJavaScriptConverter converter;
        private static IDictionary<string, object> values;
    }
}