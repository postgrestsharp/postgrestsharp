namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public interface IViewFilterElement
    {
        string RightSide { get; }
        string Operation { get; }
        string LeftSide { get; }
        string ToString();
        void SetTableName(string tableName);
    }
}