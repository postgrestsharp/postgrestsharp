namespace PostgRESTSharp.Conventions
{
    public interface IImplicitViewConvention : IImplicitConvention, IViewConvention
    {
        bool IsMatch(IViewMetaModel metaModel);
    }
}