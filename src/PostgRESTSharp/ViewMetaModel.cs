using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;

namespace PostgRESTSharp
{
    public class ViewMetaModel : IViewMetaModel
    {
        private List<ViewMetaModelColumn> columns;
        private List<ViewMetaModelSource> joinSources;
        private List<ViewFilterElement> viewFilterElements; 
       

        public ViewMetaModel(string databaseName, string schemaName, string viewName, 
            string modelName, string modelNamePluralised, string description, bool isExcluded)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.ViewName = viewName;
            this.ModelName = modelName;
            this.Description = description;
            this.ModelNamePluralised = modelNamePluralised;
            this.columns = new List<ViewMetaModelColumn>();
            this.joinSources = new List<ViewMetaModelSource>();
            this.viewFilterElements = new List<ViewFilterElement>();
            this.IsExclused = isExcluded;

        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string ViewName { get; protected set; }

        public string Description { get; protected set; }

        public string ModelName { get; protected set; }

        public string ModelNamePluralised { get; protected set; }

        public ITableMetaModel PrimarySource { get; protected set; }

        public IEnumerable<ViewMetaModelSource> JoinSources { get { return this.joinSources; } }

        public IEnumerable<ViewMetaModelColumn> Columns { get { return this.columns; } }

        public IEnumerable<ViewFilterElement> FilterElements { get { return this.viewFilterElements; } }

        public bool IsExclused { get; protected set; }
        
        public bool HasKey { get { return this.Columns.Where(x => x.IsKeyColumn).Any(); } }

        public bool HasViewKey
        {
            get
            {
                if (this.Columns.Where(x => x.IsPrimaryKeyColumn).Any())
                {
                    return true;
                }
                else
                {
                    if (this.Columns.Where(x => x.IsUniqueColumn).Count() == 1)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void AddColumn(TableMetaModelColumn storageColumn, ITableMetaModel storageColumnSource, 
            string columnAlias, bool isComplex, IJoinRelationModel joinRelationModel)
        {
            var alias = columnAlias.Length > 0 ? columnAlias : storageColumn.FieldNameCamelCased;
            var col = new ViewMetaModelColumn(storageColumn.FieldNameCamelCased, 
                alias, storageColumn.StorageDataType, 
                storageColumn.StorageDataTypeLength,
                storageColumn.FieldDataType, this.columns.Count,
                storageColumn.IsPrimaryKeyColumn || storageColumn.IsUnique ? true : false, 
				storageColumn.IsPrimaryKeyColumn,
                storageColumn.IsUnique, isComplex,
                joinRelationModel, storageColumnSource, 
                storageColumn);
            this.columns.Add(col);
        }

        public void SetPrimaryTableSource(ITableMetaModel primaryTableSource)
        {
            this.PrimarySource = primaryTableSource;
        }

        public void SetColumnToHidden(string columnName)
        {
            foreach (var column in columns.Where(x => x.ColumnName == columnName))
            {
                column.SetColumnToHidden();
            }
        }

        public void AddJoinSource(ITableMetaModel joinSource, TableMetaModelColumn joinColumn, ITableMetaModel source, TableMetaModelColumn sourceColumn)
        {
            this.joinSources.Add(new ViewMetaModelSource(joinSource, joinColumn, source, sourceColumn));
        }

        public void AddFilterElements(IEnumerable<IViewFilterElement> viewFilterElements)
        {
            if (viewFilterElements == null) return;

            foreach (var viewFilterElement in viewFilterElements)
            {
                this.viewFilterElements.Add((ViewFilterElement) viewFilterElement);    
            }

            
        }

    }
}