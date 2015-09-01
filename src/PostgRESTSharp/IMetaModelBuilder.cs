using System.Collections.Generic;

namespace PostgrestSharp
{
    public interface IMetaModelBuilder
    {
        MetaModel BuildMetaModel(InfoSchemaTable table, IEnumerable<InfoSchemaForeignKeys> foreignKeys,
                IEnumerable<InfoSchemaColumn> tableColumns);
    }
}