using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.REST
{
    public class ModelToJSONSchemaConverter
    {
        public string Convert(RESTModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("{");

            sb.AppendLine("\t\"$schema\": \"http://json-schema.org/draft-04/schema#\", ");
            sb.AppendFormat("\t\"title\": \"{0}\"\n, ", model.Name);
            sb.AppendLine("\t\"type\": \"object\", ");
            sb.AppendLine("\t\"properties\": {");

            var props = model.Properties.ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                sb.AppendFormat("\t\t\"{0}\": {\n", prop.Name);

                sb.AppendFormat("\t\t\t\"description\": \"{0}\",\n", prop.Description);
                sb.AppendFormat("\t\t\t\"type\": \"{0}\"\n", prop.Type);

                sb.Append("\t\t}");
                if (i < props.Count - 1)
                {
                    sb.AppendLine(",");
                }
            }

            sb.AppendLine("\t}");
            sb.AppendLine("\t\"required\": []");

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
