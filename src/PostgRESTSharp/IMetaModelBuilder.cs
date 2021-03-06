﻿using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IMetaModelBuilder
    {
        MetaModel BuildMetaModel(InfoSchemaTable table, IEnumerable<InfoSchemaForeignKeys> foreignKeys,
                IEnumerable<InfoSchemaColumn> tableColumns);
    }
}