using System;
using System.Collections.Generic;

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

