using System;

namespace PostgRESTSharp
{
	public class ViewMetaModelSource
	{
		public ViewMetaModelSource ()
		{
		}

		public IMetaModel JoinSource { get; protected set; }

		public MetaModelColumn JoinColumn { get; protected set; }

		public MetaModelColumn SourceColumn { get; protected set; }
	}
}

