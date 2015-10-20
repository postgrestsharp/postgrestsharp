using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
    public class DefaultTableExclusionConvention : ITableExclusionConvention, IDefaultTableConvention
    {
        public bool IsMatch(ITableMetaModel table)
        {
            return false;
        }
        public bool IsExcluded()
        {
            return false;
        }

    }
}

