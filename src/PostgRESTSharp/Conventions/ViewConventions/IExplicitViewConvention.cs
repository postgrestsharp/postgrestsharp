using System;

namespace PostgRESTSharp.Conventions
{
	public interface IExplicitViewConvention : IExplicitConvention, IViewConvention
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string ViewName { get; }
	}
}

