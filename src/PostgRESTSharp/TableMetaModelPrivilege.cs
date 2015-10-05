namespace PostgRESTSharp
{
    public class TableMetaModelPrivilege
    {
        public TableMetaModelPrivilege(string type, string grantee, bool isOwner)
        {
            this.Type = type;
            this.Grantee = grantee;
            this.IsOwner = isOwner;
        }

        public string Type { get; protected set; }

        public string Grantee { get; protected set; }
        
        public bool IsOwner { get; protected set; }


    }
}