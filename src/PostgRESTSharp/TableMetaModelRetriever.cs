using Dapper;
using System.Collections.Generic;
using System.Data;
using PostgRESTSharp.Data;
using PostgRESTSharp.Configuration;

namespace PostgRESTSharp
{
    public class TableMetaModelRetriever : ITableMetaModelRetriever
    {
        private IDbConnectionProvider connectionProvider;
        private ITableMetaModelQueryProvider dataStorageQueryProvider;
        private ITableMetaModelBuilder metamodelBuilder;

        public TableMetaModelRetriever(IDbConnectionProvider connectionProvider, ITableMetaModelQueryProvider dataStorageQueryProvider, ITableMetaModelBuilder metamodelBuilder)
        {
            this.connectionProvider = connectionProvider;
            this.dataStorageQueryProvider = dataStorageQueryProvider;
            this.metamodelBuilder = metamodelBuilder;
        }

        public IEnumerable<ITableMetaModel> RetrieveMetaModels(string databaseName, string[] includedSchemas, string[] excludedStorageObjects)
        {
            List<ITableMetaModel> metaModels = new List<ITableMetaModel>();
            Dictionary<string, ITableMetaModel> allMetaModels = new Dictionary<string, ITableMetaModel>();

            using (IDbConnection conn = this.connectionProvider.GetConnection())
            {
                conn.Open();

                // get the tables for each schema
                foreach (var schemaName in includedSchemas)
                {
                    var tablesQuery = this.dataStorageQueryProvider.GetTablesQuery(databaseName, schemaName);

                    var tables = conn.Query<InfoSchemaTable>(tablesQuery);

                    foreach (var table in tables)
                    {
                        // get the table foreign keys
                        var tableForeignKeysQuery = this.dataStorageQueryProvider.GetForeignKeysQuery(databaseName, schemaName, table.TableName);
                        var foreignKeys = conn.Query<InfoSchemaForeignKeys>(tableForeignKeysQuery);

                        // get the columns for each table
                        var columnsQuery = this.dataStorageQueryProvider.GetColumnsQuery(databaseName, schemaName, table.TableName);
                        var columns = conn.Query<InfoSchemaColumn>(columnsQuery);

                        //                        // get the column usage
                        //                        var columnKeyUsageQuery = this.dataStorageQueryProvider.GetColumnKeyUsageQuery(databaseName, schemaName, table.TableName);
                        //                        var columnUsage = conn.Query<InfoSchemaKeyColumnUsage>(columnKeyUsageQuery);

                        // get the privileges
                        var privilegesQuery = this.dataStorageQueryProvider.GetTableGrantsQuery(databaseName, schemaName, table.TableName);
                        var grants = conn.Query<InfoSchemaTableGrant>(privilegesQuery);


                        // build the metamodel
                        var model = this.metamodelBuilder.BuildMetaModel(table, foreignKeys, columns, grants);
                        metaModels.Add(model);
                        allMetaModels.Add(string.Format("{0}_{1}_{2}", databaseName, schemaName, table.TableName), model);
                    }
                }

                conn.Close();
            }

            // go back and populate the relation classes
            foreach (var model in metaModels)
            {
                foreach (var rel in model.Relations)
                {
                    string relationType = string.Format("{0}_{1}_{2}", rel.DatabaseName, rel.SchemaName, rel.RelationTableName);
                    var metaModel = allMetaModels[relationType];
                    rel.StorageModel = metaModel;
                }
            }

            return metaModels;
        }
    }
}