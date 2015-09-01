using System.Collections.Generic;

namespace PostgrestSharp
{
    public interface IMetaModelRetriever
    {
        IEnumerable<MetaModel> RetrieveMetaModels(string databaseName, string[] includedSchemas, string[] excludedStorageObjects);
    }
}