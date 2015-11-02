using System.Linq;
using System.Collections.Generic;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;


namespace PostgRESTSharp
{
	public interface IViewMetaModel
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string ViewName { get; }

        string ModelName { get; }

        string Description { get; }

        string ModelNamePluralised { get; }

        IEnumerable<ViewMetaModelColumn> Columns { get; }

		ITableMetaModel PrimarySource { get; }

		ITableMetaModel OriginalSource { get; }

		IEnumerable<ViewMetaModelSource> JoinSources { get; }

        IEnumerable<ViewFilterElement> FilterElements { get; }

	    bool IsExclused { get; }

        bool HasKey { get; }

        bool HasViewKey { get; }

        void AddColumn(TableMetaModelColumn storageColumn, ITableMetaModel storageColumnSource, string columnAlias, bool isComplexType, IJoinRelationModel joinRelationModel);

		void AddJoinSource (ITableMetaModel joinSource, TableMetaModelColumn joinColumn, ITableMetaModel source, TableMetaModelColumn sourceColumn);

        void AddFilterElements(IEnumerable<IViewFilterElement> viewFilterElement);

        void SetPrimaryTableSource(ITableMetaModel primaryTableSource);

        void SetOriginalTableSource(ITableMetaModel originalTableSource);

	    void SetColumnToHidden(string columnName);

	}
}

