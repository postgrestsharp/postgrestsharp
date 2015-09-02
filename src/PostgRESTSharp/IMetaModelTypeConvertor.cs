namespace PostgRESTSharp
{
    public interface IMetaModelTypeConvertor
    {
        string GetNativeTypeForSqlType(string sqlType);
    }
}