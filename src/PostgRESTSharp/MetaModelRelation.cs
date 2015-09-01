using System.Collections.Generic;

namespace PostgrestSharp
{
    public class MetaModelRelation
    {
        public MetaModelRelation(string databaseName, string schemaName, string tableName, string relationTableName, string keyName, IEnumerable<string> relationColumns, IEnumerable<string> uniqueColumns, RelationDirectionEnum direction)
        {
            this.DatabaseName = databaseName;
            this.SchemaName = schemaName;
            this.TableName = tableName;
            this.RelationTableName = relationTableName;
            this.KeyName = keyName;
            this.RelationColumns = new List<string>(relationColumns);
            this.UniqueColumns = new List<string>(uniqueColumns);
            this.Direction = direction;
        }

        public string DatabaseName { get; protected set; }

        public string SchemaName { get; protected set; }

        public string TableName { get; protected set; }

        public string RelationTableName { get; set; }

        public string KeyName { get; protected set; }

        public RelationDirectionEnum Direction { get; set; }

        public IEnumerable<string> RelationColumns { get; protected set; }

        public IEnumerable<string> UniqueColumns { get; protected set; }

        public IMetaModel StorageModel { get; set; }
    }
}