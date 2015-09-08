using System;

namespace PostgRESTSharp.Conventions
{
	public interface IImplicitTableConvention : IImplicitConvention
	{
		bool IsMatch(IMetaModel metaModel);
	}
}

