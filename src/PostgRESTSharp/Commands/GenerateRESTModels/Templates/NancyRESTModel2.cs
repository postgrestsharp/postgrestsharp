using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates
{
    public partial class NancyRESTModel
    {
        public NancyRESTModel(IViewMetaModel metaModel, string fileNamespace)
        {
            this.MetaModel = metaModel;
            this.Namespace = fileNamespace;
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string Namespace { get; protected set; }

        public IEnumerable<string> GetProperties()
        {
            foreach (var col in this.MetaModel.Columns)
            {
                yield return string.Format("public {0} {1} {{ get; protected set; }}", ConvertToNullableIfReq(col.ModelDataType), col.ColumnName);
            }
        }

        public string GetConstructorArgs()
        {
             return string.Join(", ",  this.MetaModel.Columns.Select(x=> string.Format("{0} {1}", ConvertToNullableIfReq(x.ModelDataType), x.ColumnName)));
        }

        public IEnumerable<string> GetConstructorAssignments()
        {
            foreach (var col in this.MetaModel.Columns)
            {
                yield return string.Format("this.{0} = {0};" , col.ColumnName);
            }
        }

        public string GetPrimaryKeyColumnName()
        {
            return this.MetaModel.Columns.Where(x => x.IsKeyColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
        }

		public IEnumerable<ViewMetaModelRelation> GetRelations()
		{
			return this.MetaModel.Columns.Where(x => x.RelatedView != null).Select(y=>y.RelatedView);
		}

		public string GetRelationUrl(ViewMetaModelRelation relation)
		{
			return relation.RelatedView.ModelNamePluralised.ToLower();
		}

		public string GetRelationName(ViewMetaModelRelation relation)
		{
			var colName = relation.RelationColumn.ColumnName.ToLower();
			if(colName.EndsWith("id"))
			{
				colName = colName.Substring(0, colName.Length - 2);
			}
			return colName;
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
