namespace PostgRESTSharp
{
    public interface ITableMetaModelTypeConvertor
    {
        string GetNativeTypeForSqlType(string sqlType);
    }
}