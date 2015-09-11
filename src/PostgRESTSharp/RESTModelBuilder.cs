using PostgRESTSharp.REST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class RESTModelBuilder : IRESTModelBuilder
    {
        public RESTModel BuildRESTModel(IViewMetaModel view, RESTVerbEnum modelType)
        {
            string modelClassName = string.Format("{0}{2}Model", view.ModelName, ConvertToRESTVerbString(modelType));
            RESTModel model = new RESTModel(modelType, view.ModelName , modelClassName, 
                GetPrimaryKeyColumnName(view),
                this.GetProperties(view, modelType),
                this.GetParameters(view, modelType));

            return model;
        }

        private string ConvertToRESTVerbString(RESTVerbEnum verb)
        {
            switch (verb)
            {
                case RESTVerbEnum.GET:
                    return "GET";

                case RESTVerbEnum.POST:
                    return "POST";

                case RESTVerbEnum.POSTResponse:
                    return "POSTResponse";

                case RESTVerbEnum.PUT:
                    return "PUT";

                case RESTVerbEnum.PATCH:
                    return "PATCH";

                case RESTVerbEnum.DELETE:
                    return "DELETE";

                default:
                    return "";
            }
        }

        private string GetPrimaryKeyColumnName(IViewMetaModel view)
        {
            // there must either be a pk or a single UK
            if (view.Columns.Where(x => x.IsPrimaryKeyColumn).Any())
            {
                return view.Columns.Where(x => x.IsPrimaryKeyColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
            }
            else
            {
                if (view.Columns.Where(x => x.IsUniqueColumn).Count() == 1)
                {
                    return view.Columns.Where(x => x.IsUniqueColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
                }
            }

            throw new Exception("View has not primary key or no single unique column");
        }

        public IEnumerable<RESTModelProperty> GetProperties(IViewMetaModel view, RESTVerbEnum type)
        {
            var columns = view.Columns;
            switch (type)
            {
                case RESTVerbEnum.POST:
                    columns = view.Columns.Where(x => !x.IsPrimaryKeyColumn);
                    break;

                case RESTVerbEnum.POSTResponse:
                    columns = view.Columns.Where(x => x.IsPrimaryKeyColumn);
                    break;
            }
            foreach (var col in columns)
            {
                yield return new RESTModelProperty(col.ColumnName, col.Description, ConvertToNullableIfReq(col.ModelDataType));
            }
        }

        public IEnumerable<RESTModelProperty> GetParameters(IViewMetaModel view, RESTVerbEnum type)
        {
            var columns = view.Columns;
            switch (type)
            {
                case RESTVerbEnum.POST:
                    columns = view.Columns.Where(x => !x.IsPrimaryKeyColumn);
                    break;

                case RESTVerbEnum.POSTResponse:
                    columns = view.Columns.Where(x => x.IsPrimaryKeyColumn);
                    break;
            }
            foreach (var col in columns)
            {
                yield return new RESTModelProperty(col.ColumnName, col.Description, ConvertToNullableIfReq(col.ModelDataType));
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