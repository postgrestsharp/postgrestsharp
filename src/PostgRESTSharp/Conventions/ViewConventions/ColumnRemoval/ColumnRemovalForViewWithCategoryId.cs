﻿using System;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class ColumnRemovalForViewWithCategoryId : IColumnRemovalConvention, IImplicitViewConvention
    {
        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.Any(a => a.ColumnName.Equals(ColumnToRemove(), StringComparison.OrdinalIgnoreCase));
        }
        
        public string ColumnToRemove()
        {
            return "categoryId";
        }
    }
}