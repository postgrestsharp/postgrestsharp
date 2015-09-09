using System;

namespace PostgRESTSharp
{
	public class ViewMetaModelSource
	{
		public ViewMetaModelSource (ITableMetaModel joinSource, TableMetaModelColumn joinColumn, ITableMetaModel source, TableMetaModelColumn sourceColumn)
		{
            this.JoinSource = joinSource;
            this.JoinColumn = joinColumn;
            this.Source = source;
            this.SourceColumn = sourceColumn;
		}

		public ITableMetaModel JoinSource { get; protected set; }

		public TableMetaModelColumn JoinColumn { get; protected set; }

        public ITableMetaModel Source { get; protected set; }

		public TableMetaModelColumn SourceColumn { get; protected set; }
	}
}

