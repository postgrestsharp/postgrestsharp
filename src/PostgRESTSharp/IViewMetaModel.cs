using System;
using System.Collections.Generic;


namespace PostgRESTSharp
{
	public interface IViewMetaModel
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string ViewName { get; }

        string ModelName { get; }

        IEnumerable<MetaModelViewColumn> Columns { get; }

		IMetaModel PrimarySource { get; }

		IEnumerable<ViewMetaModelSource> JoinSources { get; }

		void AddColumn (MetaModelColumn storageColumn, IMetaModel storageColumnSource);

		void AddJoinSource (IMetaModel joinSource, MetaModelColumn joinColumn, MetaModelColumn sourceColumn);
	}
}

