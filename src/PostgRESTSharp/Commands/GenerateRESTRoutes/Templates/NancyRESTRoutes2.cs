using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    public partial class NancyRESTRoutes
    {
        public NancyRESTRoutes(IEnumerable<IViewMetaModel> metaModels, string fileNamespace, string modelNamespace)
        {
            this.MetaModels = new List<IViewMetaModel>(metaModels);
            this.Namespace = fileNamespace;
            this.ModelNamespace = modelNamespace;
        }

        public IEnumerable<IViewMetaModel> MetaModels { get; protected set; }

        public string Namespace { get; protected set; }

        public string ModelNamespace { get; protected set; }
    }
}
