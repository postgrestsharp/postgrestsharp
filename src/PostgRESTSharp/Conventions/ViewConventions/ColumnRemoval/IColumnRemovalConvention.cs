using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public interface IColumnRemovalConvention : IViewConvention
    {
        string ColumnToRemove();
    }
    
}