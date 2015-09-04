using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates
{
    public partial class NancyRESTModels
    {
        public NancyRESTModels(IEnumerable<IViewMetaModel> metaModels, string fileNamespace)
        {
            this.MetaModels = new List<IViewMetaModel>(metaModels);
            this.Namespace = fileNamespace;
        }

        public IEnumerable<IViewMetaModel> MetaModels { get; protected set; }

        public string Namespace { get; protected set; }

        public IEnumerable<string> GetProperties(IViewMetaModel metaModel)
        {
            foreach (var col in metaModel.Columns)
            {
                yield return string.Format("public {0} {1} {{ get; protected set; }}", ConvertToNullableIfReq(col.ModelDataType), col.ColumnName);
            }
        }

        public string GetConstructorArgs(IViewMetaModel viewMetaModel)
        {
            return string.Join(", ", viewMetaModel.Columns.Select(x => string.Format("{0} {1}", ConvertToNullableIfReq(x.ModelDataType), x.ColumnName)));
        }

        public IEnumerable<string> GetConstructorAssignments(IViewMetaModel viewMetaModel)
        {
            foreach (var col in viewMetaModel.Columns)
            {
                yield return string.Format("this.{0} = {0};", col.ColumnName);
            }
        }

        public string ConvertToNullableIfReq(string fieldType)
        {
            switch (fieldType)
            {
                case "long":
                    return "long?";

                case "int":
                    return "int?";

                case "DateTime":
                    return "DateTime?";

                case "DateTimeOffset":
                    return "DateTimeOffset?";

                case "bool":
                    return "bool?";

                case "decimal":
                    return "decimal?";

                default:
                    return fieldType;
            }
        }
    }
}
