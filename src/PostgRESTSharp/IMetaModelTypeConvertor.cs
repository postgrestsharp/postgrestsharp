namespace PostgrestSharp
{
    public interface IMetaModelTypeConvertor
    {
        string GetNativeTypeForSqlType(string sqlType);
    }
}