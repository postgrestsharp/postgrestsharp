using System;

namespace PostgRESTSharp.Conventions
{
	public class ViewNameForEnumTablesConvention : IViewNameConvention, IImplicitTableConvention
	{
		public string DetermineViewName (IMetaModel metaModel)
		{
			return metaModel.ModelNameCamelCased.Replace("Enum", "");
		}

		public bool IsMatch (IMetaModel metaModel)
		{
			return metaModel.TableName.ToLower ().EndsWith ("enum");
		}
	}
}

