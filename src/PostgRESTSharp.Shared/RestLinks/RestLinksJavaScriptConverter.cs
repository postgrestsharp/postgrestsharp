using System;
using System.Collections.Generic;
using Nancy.Json;

namespace PostgRESTSharp.Shared
{
	public class RestLinksJavaScriptConverter : JavaScriptConverter
	{
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			if (obj is IRestLink)
			{
				IRestLink link = obj as IRestLink;
				var json = new Dictionary<string, object>();

				if (link is IRestSimpleLink)
				{
					ExtractUri(((IRestSimpleLink)link).Uri, json);
				}
				else
					if (link is IRestArrayLink)
					{
						var arrayLink = link as IRestArrayLink;

						foreach (var linkUri in arrayLink.Uris)
						{
						}
					}

				return json;
			}
			return null;
		}

		private void ExtractUri(IRestLinkUri uri, Dictionary<string, object> json)
		{
			if (uri is TemplatedRestLinkUri)
			{
				var templatedUri = uri as TemplatedRestLinkUri;
				json["name"] = templatedUri.Name;
				json["href"] = templatedUri.Href;
				json["templated"] = true;
			}
			else
			{
				json["href"] = uri.Href;
			}
		}

		public override IEnumerable<Type> SupportedTypes
		{
			get { yield return typeof(IRestLink); }
		}
	}
}

