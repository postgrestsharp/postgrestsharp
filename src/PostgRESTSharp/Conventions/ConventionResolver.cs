using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class ConventionResolver : IConventionResolver
	{
		public ConventionResolver (IEnumerable<IConvention> conventions)
		{
		}

		public T ResolveTableConvention<T>(IMetaModel metaModel) where T : ITableConvention; 
		{
			throw new NotImplementedException ();
		}

	}
}

