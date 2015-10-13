using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates
{
    public partial class NancyRESTModel
    {
		public NancyRESTModel(IViewMetaModel metaModel, string fileNamespace, bool isReadOnly)
        {
            this.MetaModel = metaModel;
            this.Namespace = fileNamespace;
			this.IsReadOnly = isReadOnly;
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string Namespace { get; protected set; }

		public bool IsReadOnly { get; protected set; }

		public IEnumerable<string> GetProperties(RESTModelTypeEnum type)
        {
            var columns = GetValidViewableColumns();
			switch(type) 
			{
				case RESTModelTypeEnum.Post:
					columns = this.MetaModel.Columns.Where (x => !x.IsPrimaryKeyColumn);
					break;
				case RESTModelTypeEnum.PostResponse:
					columns = this.MetaModel.Columns.Where (x => x.IsPrimaryKeyColumn);
					break;
			}
			foreach (var col in columns)
            {

                if (col.IsComplexType && col.JoinRelationModel == null) { continue; }

                if (col.IsComplexType)
                {
                    yield return string.Format("public {0} {1} {{ get; protected set; }}",
                        col.JoinRelationModel.RelatedModeType, col.JoinRelationModel.RelatedModelName);
                }
                else
                {
                    yield return string.Format("public {0} {1} {{ get; protected set; }}",
                        ConvertToNullableIfReq(col.ModelDataType), col.ColumnAlias);
                }
            }
        }

		public string GetConstructorArgs(RESTModelTypeEnum type)
        {
			var columns = GetValidViewableColumns();
			switch(type) 
			{
				case RESTModelTypeEnum.Post:
					columns = this.MetaModel.Columns.Where (x => !x.IsPrimaryKeyColumn);
					break;
				case RESTModelTypeEnum.PostResponse:
					columns = this.MetaModel.Columns.Where (x => x.IsPrimaryKeyColumn);
					break;
			}
            return string.Join(", ",  columns.Select(x=> string.Format("{0} {1}", ConvertToNullableIfReq(x.ModelDataType), x.ColumnAlias)));
        }

        private IEnumerable<ViewMetaModelColumn> GetValidViewableColumns()
        {
            return this.MetaModel.Columns.Where(x => !x.IsHidden);
        }

        public IEnumerable<string> GetConstructorAssignments(RESTModelTypeEnum type)
        {
            var columns = GetValidViewableColumns();
			switch(type) 
			{
				case RESTModelTypeEnum.Post:
					columns = this.MetaModel.Columns.Where (x => !x.IsPrimaryKeyColumn);
				break;
			case RESTModelTypeEnum.PostResponse:
					columns = this.MetaModel.Columns.Where (x => x.IsPrimaryKeyColumn);
				break;
			}
            foreach (var col in columns)
            {
                if (col.IsComplexType && col.JoinRelationModel == null) { continue; }

                if (col.IsComplexType)
                {

                    string constructor = string.Join("", col.JoinRelationModel.Fields.Select(x => string.Format("{0},", x.Key)));
                    constructor = constructor.Substring(0, constructor.Length - 1);

                    yield return string.Format("this.{0} = new {1} ({2});",
                        col.JoinRelationModel.RelatedModelName, col.JoinRelationModel.RelatedModeType, constructor);
                }
                else
                {
                    yield return string.Format("this.{0} = {0};", col.ColumnName);
                }
            }
        }

        public string GetPrimaryKeyColumnName()
        {
            // there must either be a pk or a single UK
            if(this.MetaModel.Columns.Where(x => x.IsPrimaryKeyColumn).Any())
            {
                return this.MetaModel.Columns.Where(x => x.IsPrimaryKeyColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
            }
            else
            {
                if(this.MetaModel.Columns.Where(x=>x.IsUniqueColumn).Count() == 1)
                {
                    return this.MetaModel.Columns.Where(x => x.IsUniqueColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
                }
            }

            throw new Exception("View has not primary key or no single unique column");
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
