﻿using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class ViewMetaModel : IViewMetaModel
    {
        private List<ViewMetaModelColumn> columns;
        private List<ViewMetaModelSource> joinSources;

        public ViewMetaModel(string databaseName, string schemaName, string viewName, string modelName, string modelNamePluralised)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.ViewName = viewName;
            this.ModelName = modelName;
            this.ModelNamePluralised = modelNamePluralised;
            this.columns = new List<ViewMetaModelColumn>();
            this.joinSources = new List<ViewMetaModelSource>();
        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string ViewName { get; protected set; }

        public string ModelName { get; protected set; }

        public string ModelNamePluralised { get; protected set; }

        public ITableMetaModel PrimarySource { get; protected set; }

        public IEnumerable<ViewMetaModelSource> JoinSources { get { return this.joinSources; } }

        public IEnumerable<ViewMetaModelColumn> Columns { get { return this.columns; } }

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

        public void AddColumn(TableMetaModelColumn storageColumn, ITableMetaModel storageColumnSource)
        {
            var col = new ViewMetaModelColumn(storageColumn.FieldNameCamelCased, storageColumn.StorageDataType, storageColumn.StorageDataTypeLength,
                storageColumn.FieldDataType, this.columns.Count,
                storageColumn.IsPrimaryKeyColumn || storageColumn.IsUnique ? true : false, 
				storageColumn.IsPrimaryKeyColumn,
                storageColumn.IsUnique,
                storageColumnSource, storageColumn);
            this.columns.Add(col);
        }

        public void SetPrimaryTableSource(ITableMetaModel primaryTableSource)
        {
            this.PrimarySource = primaryTableSource;
        }

        public void AddJoinSource(ITableMetaModel joinSource, TableMetaModelColumn joinColumn, ITableMetaModel source, TableMetaModelColumn sourceColumn)
        {
            this.joinSources.Add(new ViewMetaModelSource(joinSource, joinColumn, source, sourceColumn));
        }
    }
}