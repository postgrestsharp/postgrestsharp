using System;

namespace PostgRESTSharp.Conventions
{
	public class ViewNameDefaultConvention : IViewNamingConvention, IDefaultTableConvention
	{
		public string DetermineViewName (ITableMetaModel metaModel)
		{
			return metaModel.ModelNameCamelCased;
		}
	}
}
