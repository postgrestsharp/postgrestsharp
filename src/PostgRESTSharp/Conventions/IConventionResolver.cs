using System;

namespace PostgRESTSharp.Conventions
{
	public interface IConventionResolver
	{
		IConvention ResolveConvention<T>(IMetaModel metaModel) where T : IConvention; 
	}
}

