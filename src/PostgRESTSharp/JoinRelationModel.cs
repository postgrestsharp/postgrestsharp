using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class JoinRelationModel : IJoinRelationModel
    {
        public string RelatedModelName { get; set; }

        public string RelatedModeType
        {
            get
            {
                return RelatedModelName.Substring(0, 1).ToUpper() +
                       RelatedModelName.Substring(1, RelatedModelName.Length - 1) + "GETModel";
            }
        }

        public string SourceModelName { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Fields { get; set; }
    }
}