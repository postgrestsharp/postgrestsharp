namespace PostgRESTSharp.Conventions.ViewConventions
{
	public interface IExplicitViewConvention : IExplicitConvention, IViewConvention
	{
		string DatabaseName { get; }

		string SchemaName { get; }

		string ViewName { get; }
	}
}

