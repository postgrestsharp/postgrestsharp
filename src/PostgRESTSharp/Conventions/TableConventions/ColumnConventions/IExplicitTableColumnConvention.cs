using System;

namespace PostgRESTSharp.Conventions
{
	public interface IExplicitTableColumnConvention : IExplicitTableConvention
	{
		string ColumnName { get; }
	}
}

