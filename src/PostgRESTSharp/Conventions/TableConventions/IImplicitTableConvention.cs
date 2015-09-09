using System;

namespace PostgRESTSharp.Conventions
{
	public interface IImplicitTableConvention : IImplicitConvention, ITableConvention
	{
		bool IsMatch(ITableMetaModel metaModel);
	}
}

