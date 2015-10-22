using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Shared
{
    public interface IRestLinkUri
    {
        string Href { get; }
    }

    public interface IRestLinkTemplateUri : IRestLinkUri
    {
        string Name { get; }

        bool Templated { get; }
    }

    public class RestLinkUri : IRestLinkUri
    {
        public RestLinkUri(string href)
        {
            this.Href = href;
        }

        public string Href { get; protected set; }
    }

    public class TemplatedRestLinkUri : RestLinkUri, IRestLinkTemplateUri
    {
        public TemplatedRestLinkUri(string href, string name, bool templated)
            : base(href)
        {
            this.Name = name;
            this.Templated = templated;
        }

        public bool Templated { get; protected set; }

        public string Name { get; protected set; }
    }
}
