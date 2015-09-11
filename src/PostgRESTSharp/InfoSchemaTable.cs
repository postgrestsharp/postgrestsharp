namespace PostgRESTSharp
{
    public class InfoSchemaTable
    {
        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string TableType { get; set; }

        public string TableComment { get; set; }
    }
}