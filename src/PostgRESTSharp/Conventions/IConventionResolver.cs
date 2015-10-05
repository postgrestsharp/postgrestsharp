using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IConventionResolver
	{
        void Initialise(IEnumerable<IConvention> conventions);

		T ResolveTableConvention<T>(ITableMetaModel metaModel) where T : class, ITableConvention;
        IEnumerable<T> ResolveViewConventions<T>(IViewMetaModel metaModel) where T : class, IViewConvention; 
	}
}

