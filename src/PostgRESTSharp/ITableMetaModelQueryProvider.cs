namespace PostgRESTSharp
{
    public interface ITableMetaModelQueryProvider
    {
        string GetTablesQuery(string databaseName, string schemaName);

        string GetColumnsQuery(string databaseName, string schemaName, string tableName);

        string GetColumnKeyUsageQuery(string databaseName, string schemaName, string tableName);

        string GetForeignKeysQuery(string databaseName, string schemaName, string tableName);
    }
}