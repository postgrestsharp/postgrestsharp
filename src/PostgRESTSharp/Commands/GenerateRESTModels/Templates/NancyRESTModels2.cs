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

        public string GetConstructorArgs(IViewMetaModel viewMetaModel)
        {
            return string.Join(", ", viewMetaModel.Columns.Select(x => string.Format("{0} {1}", x.ModelDataType, x.ColumnName)));
        }

        public IEnumerable<string> GetConstructorAssignments(IViewMetaModel viewMetaModel)
        {
            foreach (var col in viewMetaModel.Columns)
            {
                yield return string.Format("{0} = {0};", col.ColumnName);
            }
        }
    }
}
