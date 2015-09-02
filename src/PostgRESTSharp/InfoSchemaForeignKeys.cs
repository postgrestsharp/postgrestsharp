namespace PostgRESTSharp
{
    public class InfoSchemaForeignKeys
    {
        public string FkTableCatalog { get; set; }

        public string FkTableSchema { get; set; }

        public string FkTableName { get; set; }

        public string FkConstraintName { get; set; }

        public string FkColumnName { get; set; }

        public int FkOrdinalPosition { get; set; }

        public string UqTableCatalog { get; set; }

        public string UqTableSchema { get; set; }

        public string UqTableName { get; set; }

        public string UqConstraintName { get; set; }

        public string UqColumnName { get; set; }

        public int UqOrdinalPosition { get; set; }

        public string Direction { get; set; }
    }
}