namespace PostgrestSharp
{
    public interface IMetaModelConvention
    {
    }

	public interface IMetaModelFieldNamingConvention : IMetaModelConvention
    {
        string Process(string fieldName);
    }
}