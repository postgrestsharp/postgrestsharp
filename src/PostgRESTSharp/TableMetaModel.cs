using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class TableMetaModel : ITableMetaModel
    {
        public TableMetaModel(string databaseName, string schemaName, string tableName, IEnumerable<TableMetaModelColumn> columns,
			IEnumerable<TableMetaModelRelation> relations, IEnumerable<TableMetaModelPrivilege> privileges, string modelName, string modelNameCamelCased, string modelNamePluralised, TableMetaModelTypeEnum metaModelType)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.TableName = tableName;
            this.Columns = new List<TableMetaModelColumn>(columns);
            this.Relations = new List<TableMetaModelRelation>(relations);
            this.Privileges = new List<TableMetaModelPrivilege>(privileges);
            this.ModelName = modelName;
            this.ModelNameCamelCased = modelNameCamelCased;
            this.ModelNamePluralised = modelNamePluralised;
			this.MetaModelType = metaModelType;
        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string TableName { get; protected set; }

        public string RawComment { get; protected set; }

		public TableMetaModelTypeEnum MetaModelType { get; }

        public IEnumerable<TableMetaModelColumn> Columns { get; protected set; }

        public IEnumerable<TableMetaModelRelation> Relations { get; protected set; }

        public IEnumerable<TableMetaModelPrivilege> Privileges { get; protected set; }

        public IEnumerable<TableMetaModelColumn> InsertColumns { get { return this.Columns.Where(x => x.IsAutoIncrementColumn == false && x.IsUpdateable == true); } }

        public IEnumerable<TableMetaModelColumn> UpdateColumns { get { return this.Columns.Where(x => x.IsAutoIncrementColumn == false && (x.IsUpdateable == true || x.ColumnName == "modified")); } }

        public IEnumerable<TableMetaModelColumn> PrimaryKeyColumns { get { return this.Columns.Where(x => x.IsPrimaryKeyColumn == true).OrderBy(x => x.Order); } }

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