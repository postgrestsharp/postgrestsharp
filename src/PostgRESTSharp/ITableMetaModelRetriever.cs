using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface ITableMetaModelRetriever
    {
        IEnumerable<ITableMetaModel> RetrieveMetaModels(string databaseName, string[] includedSchemas, string[] excludedStorageObjects);
    }
}