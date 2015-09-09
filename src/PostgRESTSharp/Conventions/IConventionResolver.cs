using System;

namespace PostgRESTSharp.Conventions
{
	public interface IConventionResolver
	{
		T ResolveTableConvention<T>(IMetaModel metaModel) where T : class, ITableConvention; 
	}
}

