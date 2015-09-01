using System.Collections.Generic;
using System.Linq;

namespace PostgrestSharp
{
    public class MetaModel : IMetaModel
    {
        public MetaModel(string databaseName, string schemaName, string tableName, IEnumerable<MetaModelColumn> columns,
			IEnumerable<MetaModelRelation> relations, string modelName, string modelNameCamelCased, string modelNamePluralised, MetaModelTypeEnum metaModelType)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.TableName = tableName;
            this.Columns = new List<MetaModelColumn>(columns);
            this.Relations = new List<MetaModelRelation>(relations);
            this.ModelName = modelName;
            this.ModelNameCamelCased = modelNameCamelCased;
            this.ModelNamePluralised = modelNamePluralised;
			this.MetaModelType = metaModelType;
        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string TableName { get; protected set; }

		public MetaModelTypeEnum MetaModelType { get; }

        public IEnumerable<MetaModelColumn> Columns { get; protected set; }

        public IEnumerable<MetaModelRelation> Relations { get; protected set; }

        public IEnumerable<MetaModelColumn> InsertColumns { get { return this.Columns.Where(x => x.IsAutoIncrementColumn == false && x.IsUpdateable == true); } }

        public IEnumerable<MetaModelColumn> UpdateColumns { get { return this.Columns.Where(x => x.IsAutoIncrementColumn == false && (x.IsUpdateable == true || x.ColumnName == "modified")); } }

        public IEnumerable<MetaModelColumn> PrimaryKeyColumns { get { return this.Columns.Where(x => x.IsPrimaryKeyColumn == true).OrderBy(x => x.Order); } }

        public string ModelName { get; protected set; }

        public string ModelNameCamelCased { get; protected set; }

        public string ModelNamePluralised { get; protected set; }

        public bool HasPrimaryKeys()
        {
            return this.PrimaryKeyColumns.Count() > 0;
        }

        public bool HasAutoIncrementPrimaryKey()
        {
            return this.PrimaryKeyColumns.Where(x => x.IsAutoIncrementColumn).Count() > 0;
        }
    }
}