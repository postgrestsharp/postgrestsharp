namespace PostgRESTSharp.Conventions.ViewConventions
{
    public interface IImplicitViewConvention : IImplicitConvention, IViewConvention
    {
        bool IsMatch(IViewMetaModel metaModel);
    }
}