using System;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public class ViewMetaModel : IViewMetaModel
	{
		private List<MetaModelViewColumn> columns;
		private Dictionary<IMetaModel, char> aliases;
		public ViewMetaModel (string databaseName, string schemaName, string viewName)
		{
			this.DatabaseName = databaseName;
			this.SchemaName = schemaName;
			this.ViewName = viewName;
			this.columns = new List<MetaModelViewColumn>();
			this.aliases = new Dictionary<IMetaModel, char> ();
		}

		public string DatabaseName { get; protected set; }

		public string SchemaName { get; protected set; }

		public string ViewName { get; protected set; }

		public IMetaModel PrimarySource { get; protected set;}

		public IEnumerable<ViewMetaModelSource> JoinSources { get; protected set;}

		public IEnumerable<MetaModelViewColumn> Columns { get { return this.columns; } }

		public void AddColumn(MetaModelColumn storageColumn, IMetaModel storageColumnSource)
		{
			char alias;
			var gotAlias = this.aliases.TryGetValue(storageColumnSource, out alias);
			if(gotAlias)
			{
				var col = new MetaModelViewColumn (storageColumn.FieldNameCamelCased, storageColumn.StorageDataType, storageColumn.StorageDataTypeLength, this.columns.Count,
					storageColumn.IsPrimaryKeyColumn || storageColumn.IsUnique ? true : false, alias.ToString (), new ViewMetaModelRelation[] {});
				this.columns.Add (col);
			}
		}

		public void SetPrimaryTableSource(IMetaModel primaryTableSource)
		{
			this.PrimarySource = primaryTableSource;
			this.aliases.Add(primaryTableSource, 'a');
		}

		public void AddJoinSource (IMetaModel joinSource, MetaModelColumn joinColumn, MetaModelColumn sourceColumn)
		{
			
		}
	}
}

