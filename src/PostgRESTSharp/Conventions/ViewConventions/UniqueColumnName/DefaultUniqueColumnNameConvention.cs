using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;
using PostgRESTSharp.Core.Conventions;

namespace PostgRESTSharp.Conventions
{
    public class DefaultUniqueColumnNameConvention : IUniqueColumnNameConvention, IDefaultViewConvention
	{
        public void ApplyUniqueColumn(IViewMetaModel metaModel)
        {

            return;
            foreach (var duplicateColumn in ConventionHelper.GetListOfDuplicateModelColumns(metaModel))
            {
                metaModel.Columns.Where(x=> x.ColumnName.Equals(duplicateColumn.ColumnName))
                    .Each(x => x.SetColumnToHidden());

                metaModel.Columns.First().SetColumnToVisisble();

            }
        }
	}
}

