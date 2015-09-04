using Nancy;

namespace PostgRESTSharp.Shared
{
    public interface IRestLinkBuilder
    {
        void AddSelfLink(RestLinks links, Url currentUrl);

        void AddSelfLinkForNewEntity(RestLinks links, Url currentUrl, string entityId);

        void AddParentLink(RestLinks links, Url currentUrl);

        void AddRootLink(RestLinks links, Url currentUrl, string rootUrlPart, string linkName, string linkId);
    }
}