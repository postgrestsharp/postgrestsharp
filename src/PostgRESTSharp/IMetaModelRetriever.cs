using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IMetaModelRetriever
    {
        IEnumerable<MetaModel> RetrieveMetaModels(string databaseName, string[] includedSchemas, string[] excludedStorageObjects);
    }
}