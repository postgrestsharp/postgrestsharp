
namespace PostgRESTSharp.Pgsql
{
    public class PgSqlDataStorageTypeConvertor : ITableMetaModelTypeConvertor
    {
        public string GetNativeTypeForSqlType(string sqlType)
        {
            switch (sqlType)
            {
                case "bigint":
                    return "long";

                case "int":
                case "integer":
                case "smallint":
                    return "int";

                case "character":
                case "text":
                    return "string";

                case "timestamp":
                    return "DateTime";

                case "numeric":
                    return "decimal";

                case "boolean":
                    return "bool";

                case "timestamp with time zone":
                    return "DateTimeOffset";

                default:
                    return "string";
            }
        }
    }
}