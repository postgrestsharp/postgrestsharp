using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
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

