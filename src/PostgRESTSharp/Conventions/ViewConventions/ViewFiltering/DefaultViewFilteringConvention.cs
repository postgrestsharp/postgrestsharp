using System;
using System.Collections.Generic;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewFilteringConvention : IViewFilteringConvention, IDefaultViewConvention
	{
	    public IEnumerable<IViewFilterElement> FilterElements()
	    {
            //default filtering convention is to not filter
	        return null;
	    }
	}
}

