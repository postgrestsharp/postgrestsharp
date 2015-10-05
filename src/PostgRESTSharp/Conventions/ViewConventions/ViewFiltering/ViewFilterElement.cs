using Microsoft.SqlServer.Server;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public class ViewFilterElement : IViewFilterElement
    {
        public ViewFilterElement(string leftSide, string operation, string rightSide)
        {
            RightSide = rightSide;
            Operation = operation;
            LeftSide = leftSide;
        }

        public string RightSide { get; protected set; }
        public string Operation { get; protected set; }
        public string LeftSide { get; protected set; }
        public string TableName { get; protected set; }

        public new string ToString()
        {
            return string.Format(LeftSide + " " + Operation + " " + RightSide, TableName);
        }

        public void SetTableName(string tableName)
        {
            this.TableName = tableName;
        }
        
    }

}