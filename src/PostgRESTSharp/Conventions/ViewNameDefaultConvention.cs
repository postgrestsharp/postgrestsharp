using System;

namespace PostgRESTSharp.Conventions
{
	public class ViewNameDefaultConvention : IViewNameConvention, IDefaultTableConvention
	{
		public string DetermineViewName (IMetaModel metaModel)
		{
			return metaModel.ModelNameCamelCased;
		}
	}
}
