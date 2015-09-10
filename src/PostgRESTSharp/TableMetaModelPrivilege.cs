namespace PostgRESTSharp
{
    public class TableMetaModelPrivilege
    {
        public TableMetaModelPrivilege(string type, string grantee)
        {
            this.Type = type;
            this.Grantee = grantee;
        }

        public string Type { get; protected set; }

        public string Grantee { get; protected set; }
    }
}