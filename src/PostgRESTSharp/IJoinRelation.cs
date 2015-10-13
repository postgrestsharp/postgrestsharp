using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IJoinRelationModel
    {
        string RelatedModelName { get; set; } 
        string RelatedModeType { get; } 
        string SourceModelName { get; set; }
        IEnumerable<KeyValuePair<string, string>> Fields { get; set; }
    }
}