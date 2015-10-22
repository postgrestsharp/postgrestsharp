using Nancy;
using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Shared
{
    public class RestLinkBuilder : IRestLinkBuilder
    {
        public void AddSelfLink(RestLinks links, Nancy.Url currentUrl)
        {
            links.Add("self", new SimpleRestLink(string.Format("{0}", currentUrl)));
        }

        public void AddSelfLinkForNewEntity(RestLinks links, Url currentUrl, string entityId)
        {
            links.Add("self", new SimpleRestLink(string.Format("{0}/{1}", currentUrl, entityId)));
        }

        public void AddRootLink(RestLinks links, Url currentUrl, string rootUrlPart, string linkName, string linkId)
        {
            Uri url = new Uri(currentUrl);
            var segments = new List<string>();
            
            segments.Insert(0, url.Authority);
            segments.Insert(0, "://");
            segments.Insert(0, url.Scheme);
            links.Add(linkName, new SimpleRestLink(string.Format("{0}/{1}/{2}", string.Join("", segments), rootUrlPart, linkId)));
        }

        public void AddParentLink(RestLinks links, Nancy.Url currentUrl)
        {
            Uri url = new Uri(currentUrl);
            List<string> segments = new List<string>(url.Segments);
            segments.RemoveAt(segments.Count - 1);
            segments.Insert(0, url.Authority);
            segments.Insert(0, "://");
            segments.Insert(0, url.Scheme);
            links.Add("parent", new SimpleRestLink(string.Format("{0}", string.Join("", segments))));
        }
    }
}