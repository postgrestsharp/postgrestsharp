using System;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Core.Conventions;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{

    public class UniqueColumnNameIdConvention : IUniqueColumnNameConvention, IImplicitViewConvention
    {

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return ConventionHelper.GetListOfDuplicateModelColumnsForSpecificName(metaModel, "id").Any();
        }

        public void ApplyUniqueColumn(IViewMetaModel metaModel)
        {

            //metaModel.ModelName

            return ;
        }
        

    }
}