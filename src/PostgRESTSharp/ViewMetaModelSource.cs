using System;

namespace PostgRESTSharp
{
	public class ViewMetaModelSource
	{
		public ViewMetaModelSource (IMetaModel joinSource, MetaModelColumn joinColumn, MetaModelColumn sourceColumn)
		{
            this.JoinSource = joinSource;
            this.JoinColumn = joinColumn;
            this.SourceColumn = sourceColumn;
		}

		public IMetaModel JoinSource { get; protected set; }

		public MetaModelColumn JoinColumn { get; protected set; }

		public MetaModelColumn SourceColumn { get; protected set; }
	}
}

