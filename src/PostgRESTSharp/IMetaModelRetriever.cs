using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IMetaModelRetriever
    {
        IEnumerable<IMetaModel> RetrieveMetaModels(string databaseName, string[] includedSchemas, string[] excludedStorageObjects);
    }
}