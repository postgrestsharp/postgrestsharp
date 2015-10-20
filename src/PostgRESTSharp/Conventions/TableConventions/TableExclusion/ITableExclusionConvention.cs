namespace PostgRESTSharp.Conventions
{
    public interface ITableExclusionConvention: ITableConvention
    {
        bool IsExcluded();
    }
}