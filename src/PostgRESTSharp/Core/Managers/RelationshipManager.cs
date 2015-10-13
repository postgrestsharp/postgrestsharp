using System;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Core.Managers
{
    public static class RelationshipManager
    {
        public static void AddLookupRelationships(IViewMetaModel viewToBuild, ITableMetaModel table, string databaseName, string schemaName, IEnumerable<ITableMetaModel> additionalStorageModels)
        {

            if (table.TableName.EndsWith("_enum", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var lookupRelations = table.Relations
                        .Where(
                            x =>
                                x.Direction == RelationDirectionEnum.Forward &&
                                x.RelationTableName.ToLower().EndsWith("_enum"))
                        .ToList();

            AddLookupRelationships(viewToBuild, databaseName, schemaName, additionalStorageModels, lookupRelations, table);
        }

        public static void AddLookupRelationships(IViewMetaModel viewToBuild, string databaseName, string schemaName,
            IEnumerable<ITableMetaModel> additionalStorageModels, List<TableMetaModelRelation> lookupRelations, ITableMetaModel sourceTable)
        {
            if (lookupRelations != null)
            {
                foreach (var lookupRelation in lookupRelations)
                {
                    // get the source column
                    var sourceColumnName = lookupRelation.RelationColumns.FirstOrDefault();
                    var sourceColumn = sourceTable.Columns.Where(x => x.ColumnName == sourceColumnName).FirstOrDefault();

                    // get the related table column
                    var joinColumnName = lookupRelation.UniqueColumns.FirstOrDefault();
                    var joinTable =
                        additionalStorageModels.Where(
                            x =>
                                x.DatabaseName == databaseName && x.SchemaName == schemaName &&
                                x.TableName == lookupRelation.RelationTableName).FirstOrDefault();
                    var joinColumn = joinTable.Columns.FirstOrDefault(x => x.ColumnName == joinColumnName);

                    viewToBuild.AddJoinSource(joinTable, joinColumn, sourceTable, sourceColumn);
                }
            }
        }

    }
}