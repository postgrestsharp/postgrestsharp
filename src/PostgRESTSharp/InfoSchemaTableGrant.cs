namespace PostgRESTSharp
{
    public class InfoSchemaTableGrant
    {
        public string Grantor { get; set; }

        public string Grantee { get; set; }

        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string PrivilegeType { get; set; }

        public bool IsOwner { get; set; }
    }
}