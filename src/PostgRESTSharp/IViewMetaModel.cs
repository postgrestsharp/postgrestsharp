using System.Linq;
using System.Collections.Generic;


namespace PostgRESTSharp
{
	public interface IViewMetaModel
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string ViewName { get; }

        string ModelName { get; }

        string ModelNamePluralised { get; }

        IEnumerable<ViewMetaModelColumn> Columns { get; }

		ITableMetaModel PrimarySource { get; }

		IEnumerable<ViewMetaModelSource> JoinSources { get; }

        bool HasKey { get; }

        bool HasViewKey { get; }

        void AddColumn (TableMetaModelColumn storageColumn, ITableMetaModel storageColumnSource);

		void AddJoinSource (ITableMetaModel joinSource, TableMetaModelColumn joinColumn, ITableMetaModel source, TableMetaModelColumn sourceColumn);

        void SetPrimaryTableSource(ITableMetaModel primaryTableSource);
    }
}

