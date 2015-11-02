using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter.Mock
{
    public class RestArrayLink : IRestArrayLink
    {
        public IRestLinkUri[] Uris { get; set; }
    }
}