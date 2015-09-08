using System;

namespace PostgRESTSharp.Conventions
{
	public interface IViewNameConvention : IConvention
	{
		string DetermineViewName (IMetaModel metaModel);
	}
}

