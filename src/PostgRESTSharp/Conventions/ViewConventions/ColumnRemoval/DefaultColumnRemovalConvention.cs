using System;
using System.Collections.Generic;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;

namespace PostgRESTSharp.Conventions
{
    public class DefaultColumnRemovalConvention : IColumnRemovalConvention, IDefaultViewConvention
	{
	    public string ColumnToRemove()
	    {
	        return "";
	    }
	}
}

