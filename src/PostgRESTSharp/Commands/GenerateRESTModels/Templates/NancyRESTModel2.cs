using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates
{
    public partial class NancyRESTModel
    {
        public NancyRESTModel(IViewMetaModel metaModel, string fileNamespace)
        {
            this.MetaModel = metaModel;
            this.Namespace = fileNamespace;
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string Namespace { get; protected set; }

        public IEnumerable<string> GetProperties()
        {
            foreach (var col in this.MetaModel.Columns)
            {
                yield return string.Format("public {0} {1} {{ get; protected set; }}", col.ModelDataType, col.ColumnName);
            }
        }

        public string GetConstructorArgs()
        {
             return string.Join(", ",  this.MetaModel.Columns.Select(x=> string.Format("{0} {1}", x.ModelDataType, x.ColumnName)));
        }

        public IEnumerable<string> GetConstructorAssignments()
        {
            foreach (var col in this.MetaModel.Columns)
            {
                yield return string.Format("{0} = {0};" , col.ColumnName);
            }
        }
    }
}
