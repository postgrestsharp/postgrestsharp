using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestSharpNewtonsoftJsonSerialiserSpecs
{
    public class when_serialising : WithFakes
    {
        Establish that = () =>
        {
            jsonSerialiser = An<Newtonsoft.Json.JsonSerializer>();

            serialiser = new RestSharpNewtonsoftJsonSerialiser(jsonSerialiser);

            objectToSerialiseDynamic = new ExpandoObject();
            objectToSerialiseDynamic.id = 1;
            objectToSerialiseDynamic.description = "hello";
            objectToSerialise = (object)objectToSerialiseDynamic;
        };

        private Because of = () =>
        {
            json = serialiser.Serialize(objectToSerialise);
            jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
        };

        private It should_have_a_non_empty_json_result = () =>
        {
            json.ShouldNotBeEmpty();
        };

        private It should_have_matching_properties_to_the_original_serialised_object = () =>
        {
            //this is the only way we know that the Serialise method was called, since it's non-virtual
            var id = (int)jsonObject.id;
            id.ShouldEqual((int)objectToSerialiseDynamic.id);

            var description = jsonObject.description as string;
            description.ShouldEqual(objectToSerialiseDynamic.description as string);
        };

        private static RestSharpNewtonsoftJsonSerialiser serialiser;
        private static Newtonsoft.Json.JsonSerializer jsonSerialiser;
        private static dynamic objectToSerialiseDynamic;
        private static object objectToSerialise;
        private static string json;
        private static dynamic jsonObject;
    }
}
