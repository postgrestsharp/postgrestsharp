using Newtonsoft.Json;

namespace PostgRESTSharp.Shared.Specs.RestLinkSpecs.JsonConverter.Mock
{
    [JsonConverter(typeof(RestLinkJsonConverter))]
    class RestLinkThatDoesntImplementIRestLink
    {
        public string Name { get; set; }
        public string Href { get; set; }
    }
}