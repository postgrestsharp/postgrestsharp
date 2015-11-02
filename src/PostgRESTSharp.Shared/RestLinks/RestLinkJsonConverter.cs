using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PostgRESTSharp.Shared
{
    public class RestLinkJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var restLink = value as IRestLink;
            if (restLink == null)
            {
                return;
            }
            var simpleRestLink = value as IRestSimpleLink;
            if (simpleRestLink != null)
            {
                var uri = simpleRestLink.Uri;
                var templatedUri = uri as IRestLinkTemplateUri;
                if (templatedUri != null)
                {
                    Write(writer, templatedUri);
                }
                else
                {
                    Write(writer, uri);
                }
                return;
            }
            var array = value as IRestArrayLink;
            if (array != null)
            {
                //TODO: return array of links
                return;
            }
        }

        private void Write(JsonWriter writer, IRestLinkUri uri)
        {
            writer.WriteStartObject();
            {
                writer.WritePropertyName("href");
                writer.WriteValue(uri.Href);
            }
            writer.WriteEndObject();
        }

        private void Write(JsonWriter writer, IRestLinkTemplateUri uri)
        {
            writer.WriteStartObject();
            {
                writer.WritePropertyName("name");
                writer.WriteValue(uri.Name);
                writer.WritePropertyName("href");
                writer.WriteValue(uri.Href);
                writer.WritePropertyName("templated");
                writer.WriteValue(uri.Templated);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var message = string.Format("Deserialising {0} is not yet supported", typeof(IRestLink).Name);
            throw new NotImplementedException(message);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IRestLink).IsAssignableFrom(objectType);
        }
    }
}
