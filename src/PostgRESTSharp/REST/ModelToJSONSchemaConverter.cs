using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.REST
{
    public class ModelToJSONSchemaConverter : IModelToJSONSchemaConverter
    {
        public string Convert(RESTModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("{");

            sb.AppendLine("  \"$schema\": \"http://json-schema.org/draft-04/schema#\", ");
            sb.AppendFormat("  \"title\": \"{0}\",\n", model.Name);
            sb.AppendFormat("  \"description\": \"{0}\",\n", model.Description);
            sb.AppendLine("  \"type\": \"object\", ");
            sb.AppendLine("  \"properties\": {");

            var props = model.Properties.ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                sb.AppendFormat("    \"{0}\": {{\n", prop.Name);

                sb.AppendFormat("      \"description\": \"{0}\",\n", prop.Description);
                sb.AppendFormat("      \"type\": \"{0}\"\n", prop.Type);

                sb.Append("    }");
                if (i < props.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("  },");
            sb.AppendLine("  \"required\": []");

            sb.AppendLine("}");

            return sb.ToString();
        }


        public string ConvertCollection(RESTModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("{");

            sb.AppendLine("  \"$schema\": \"http://json-schema.org/draft-04/schema#\", ");
            sb.AppendFormat("  \"title\": \"{0}\",\n", model.Name);
            sb.AppendFormat("  \"description\": \"{0}\",\n", model.Description);
            sb.AppendLine("  \"type\": \"array\",");
            sb.AppendLine("  \"items\": {");

            sb.AppendLine("    \"type\": \"object\", ");
            sb.AppendLine("    \"properties\": {");

            var props = model.Properties.ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                sb.AppendFormat("      \"{0}\": {{\n", prop.Name);

                sb.AppendFormat("        \"description\": \"{0}\",\n", prop.Description);
                sb.AppendFormat("        \"type\": \"{0}\"\n", prop.Type);

                sb.Append("      }");
                if (i < props.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("    },");
            sb.AppendLine("    \"required\": []");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
