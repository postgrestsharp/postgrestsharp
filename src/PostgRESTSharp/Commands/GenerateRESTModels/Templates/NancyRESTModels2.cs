using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates
{
    public partial class NancyRESTModels
    {
        public NancyRESTModels(IEnumerable<IViewMetaModel> metaModels, string fileNamespace)
        {
            this.MetaModels = new List<IViewMetaModel>(metaModels);
            this.Namespace = fileNamespace;
        }

        public IEnumerable<IViewMetaModel> MetaModels { get; protected set; }

        public string Namespace { get; protected set; }

        public IEnumerable<string> GetProperties(IViewMetaModel metaModel)
        {
            foreach (var col in metaModel.Columns)
            {
                yield return string.Format("public {0} {1} {{ get; protected set; }}", col.ModelDataType, col.ColumnName);
            }
        }
    }
}
