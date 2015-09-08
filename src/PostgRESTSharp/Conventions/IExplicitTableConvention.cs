using System;

namespace PostgRESTSharp.Conventions
{
	public interface IExplicitTableConvention : IExplicitConvention
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string TableName { get; }
	}
}

