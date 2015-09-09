using System;

namespace PostgRESTSharp.Conventions
{
	public interface IConventionResolver
	{
		T ResolveTableConvention<T>(ITableMetaModel metaModel) where T : class, ITableConvention; 
	}
}

