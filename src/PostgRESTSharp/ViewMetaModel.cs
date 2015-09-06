using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class ViewMetaModel : IViewMetaModel
    {
        private List<MetaModelViewColumn> columns;
        private List<ViewMetaModelSource> joinSources;

        public ViewMetaModel(string databaseName, string schemaName, string viewName, string modelName, string modelNamePluralised)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.ViewName = viewName;
            this.ModelName = modelName;
            this.ModelNamePluralised = modelNamePluralised;
            this.columns = new List<MetaModelViewColumn>();
            this.joinSources = new List<ViewMetaModelSource>();
        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string ViewName { get; protected set; }

        public string ModelName { get; protected set; }

        public string ModelNamePluralised { get; protected set; }

        public IMetaModel PrimarySource { get; protected set; }

        public IEnumerable<ViewMetaModelSource> JoinSources { get { return this.joinSources; } }

        public IEnumerable<MetaModelViewColumn> Columns { get { return this.columns; } }

        public bool HasKey { get { return this.Columns.Where(x => x.IsKeyColumn).Any(); } }

        public void AddColumn(MetaModelColumn storageColumn, IMetaModel storageColumnSource)
        {
            var col = new MetaModelViewColumn(storageColumn.FieldNameCamelCased, storageColumn.StorageDataType, storageColumn.StorageDataTypeLength,
                storageColumn.FieldDataType, this.columns.Count,
                storageColumn.IsPrimaryKeyColumn || storageColumn.IsUnique ? true : false, 
				storageColumn.IsPrimaryKeyColumn,
				storageColumnSource, storageColumn);
            this.columns.Add(col);
        }

        public void SetPrimaryTableSource(IMetaModel primaryTableSource)
        {
            this.PrimarySource = primaryTableSource;
        }

        public void AddJoinSource(IMetaModel joinSource, MetaModelColumn joinColumn, IMetaModel source, MetaModelColumn sourceColumn)
        {
            this.joinSources.Add(new ViewMetaModelSource(joinSource, joinColumn, source, sourceColumn));
        }
    }
}