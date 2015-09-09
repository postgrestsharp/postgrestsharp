using System;

namespace PostgRESTSharp.Conventions
{
	public class ViewNameDefaultConvention : IViewNamingConvention, IDefaultTableConvention
	{
		public string DetermineViewName (IMetaModel metaModel)
		{
			return metaModel.ModelNameCamelCased;
		}
	}
}
