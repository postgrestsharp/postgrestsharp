using System;

namespace PostgRESTSharp
{
	public class ViewMetaModelSource
	{
		public ViewMetaModelSource (IMetaModel joinSource, MetaModelColumn joinColumn, IMetaModel source, MetaModelColumn sourceColumn)
		{
            this.JoinSource = joinSource;
            this.JoinColumn = joinColumn;
            this.Source = source;
            this.SourceColumn = sourceColumn;
		}

		public IMetaModel JoinSource { get; protected set; }

		public MetaModelColumn JoinColumn { get; protected set; }

        public IMetaModel Source { get; protected set; }

		public MetaModelColumn SourceColumn { get; protected set; }
	}
}

