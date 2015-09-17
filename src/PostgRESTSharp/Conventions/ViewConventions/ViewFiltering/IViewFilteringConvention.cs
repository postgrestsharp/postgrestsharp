using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public interface IViewFilteringConvention : IViewConvention
    {
        IEnumerable<IViewFilterElement> FilterElements();
    }
}