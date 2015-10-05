using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public interface IUniqueColumnNameConvention : IViewConvention
    {
        void ApplyUniqueColumn(IViewMetaModel metaModel);
    }
    
}