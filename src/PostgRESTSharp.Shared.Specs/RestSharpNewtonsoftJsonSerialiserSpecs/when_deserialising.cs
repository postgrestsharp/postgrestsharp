using System.Dynamic;
using Machine.Fakes;
using Machine.Specifications;
using Newtonsoft.Json;
using RestSharp;

namespace PostgRESTSharp.Shared.Specs.RestSharpNewtonsoftJsonSerialiserSpecs
{
    public class when_deserialising : WithFakes
    {
        Establish that = () =>
        {
            jsonSerialiser = An<Newtonsoft.Json.JsonSerializer>();

            serialiser = new RestSharpNewtonsoftJsonSerialiser(jsonSerialiser);

            objectToDeserialiseDynamic = new ExpandoObject();
            objectToDeserialiseDynamic.id = 1;
            objectToDeserialiseDynamic.description = "hello";
            objectToDeserialise = (object)objectToDeserialiseDynamic;

            response = An<IRestResponse>();
            response.Content = JsonConvert.SerializeObject(objectToDeserialise);
        };

        private Because of = () =>
        {
            jsonObject = serialiser.Deserialize<ExpandoObject>(response);
        };

        private It should_have_matching_properties_to_the_original_serialised_object = () =>
        {
            //this is the only way we know that the Deserialise method was called, since it's non-virtual
            var id = (int)jsonObject.id;
            id.ShouldEqual((int)objectToDeserialiseDynamic.id);

            var description = jsonObject.description as string;
            description.ShouldEqual(objectToDeserialiseDynamic.description as string);
        };

        private static RestSharpNewtonsoftJsonSerialiser serialiser;
        private static Newtonsoft.Json.JsonSerializer jsonSerialiser;
        private static dynamic objectToDeserialiseDynamic;
        private static object objectToDeserialise;
        private static dynamic jsonObject;
        private static IRestResponse response;
    }
}