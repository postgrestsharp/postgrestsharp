using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PostgRESTSharp.Shared
{
    [JsonConverter(typeof(RestLinkJsonConverter))]
    public interface IRestLink
    {
    }

    public interface IRestSimpleLink : IRestLink
    {
        IRestLinkUri Uri { get; }
    }

    public interface IRestArrayLink : IRestLink
    {
        IRestLinkUri[] Uris { get; }
    }

    public class SimpleRestLink : IRestSimpleLink
    {
        public SimpleRestLink(string href)
        {
            this.Uri = new RestLinkUri(href);
        }

        public SimpleRestLink(string name, string href, bool templated)
        {
            this.Uri = new TemplatedRestLinkUri(href, name, templated);
        }

        public IRestLinkUri Uri { get; protected set; }
    }

    public class RestLinks : Dictionary<string, IRestLink>
    {
        public RestLinks()
        {
        }

        public void AddLink(string name, IRestLink link)
        {
            this.Add(name, link);
        }
    }
}

