using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface ITableMetaModelBuilder
    {
        TableMetaModel BuildMetaModel(InfoSchemaTable table, IEnumerable<InfoSchemaForeignKeys> foreignKeys,
                IEnumerable<InfoSchemaColumn> tableColumns, IEnumerable<InfoSchemaTableGrant> tablePrivileges);
    }
}