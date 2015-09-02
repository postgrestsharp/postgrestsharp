using System;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public class ViewMetaModel : IViewMetaModel
	{
		private List<MetaModelViewColumn> columns;
		public ViewMetaModel (string databaseName, string schemaName, string viewName)
		{
			this.DatabaseName = databaseName;
			this.SchemaName = schemaName;
			this.ViewName = viewName;
			this.columns = new List<MetaModelViewColumn>();
		}

		public string DatabaseName { get; protected set; }

		public string SchemaName { get; protected set; }

		public string ViewName { get; protected set; }

		public IMetaModel PrimarySource { get; protected set;}

		public IEnumerable<ViewMetaModelSource> JoinSources { get; protected set;}

		public IEnumerable<MetaModelViewColumn> Columns { get { return this.columns; } }

		public void AddColumn(MetaModelColumn storageColumn, IMetaModel storageColumnSource)
		{

				var col = new MetaModelViewColumn (storageColumn.FieldNameCamelCased, storageColumn.StorageDataType, storageColumn.StorageDataTypeLength, this.columns.Count,
					storageColumn.IsPrimaryKeyColumn || storageColumn.IsUnique ? true : false, storageColumnSource, storageColumn,
                    new ViewMetaModelRelation[] {});
				this.columns.Add (col);

		}

		public void SetPrimaryTableSource(IMetaModel primaryTableSource)
		{
			this.PrimarySource = primaryTableSource;
		}

		public void AddJoinSource (IMetaModel joinSource, MetaModelColumn joinColumn, MetaModelColumn sourceColumn)
		{
			
		}
	}
}

